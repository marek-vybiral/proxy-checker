using Eto.Drawing;
using Eto.Forms;

namespace ProxyChecker;

public sealed class MainForm : Form
{
    private readonly ProxyDataModel _model = new();
    private Settings _settings = new();
    private readonly GridView _grid;
    private readonly TextArea _logBox;
    private readonly PixelLayout _layout;
    private readonly Button _btnAdd;
    private readonly Button _btnClear;
    private readonly Button _btnSettings;
    private readonly Button _btnStartTest;
    private readonly Button _btnExport;
    private TestProgressForm? _progressForm;
    private CancellationTokenSource? _cts;

    public MainForm()
    {
        Title = "ProxyChecker";
        ClientSize = new Size(484, 609);
        MinimumSize = new Size(500, 300);

        _grid = BuildGrid();
        _logBox = new TextArea { ReadOnly = true };

        _btnAdd = new Button { Text = "Add", Size = new Size(75, 23) };
        _btnAdd.Click += async (_, _) => await OnAddProxy();

        _btnClear = new Button { Text = "Clear", Size = new Size(75, 23) };
        _btnClear.Click += (_, _) => { _model.RemoveAll(); Log("Cleared"); };

        _btnSettings = new Button { Text = "Settings", Size = new Size(75, 23) };
        _btnSettings.Click += async (_, _) => await OnSettings();

        _btnStartTest = new Button { Text = "Start Test", Size = new Size(75, 23) };
        _btnStartTest.Click += async (_, _) => await StartTestAsync();

        _btnExport = new Button { Text = "Export", Size = new Size(75, 23) };
        _btnExport.Click += (_, _) => Export();

        _layout = new PixelLayout();
        _layout.Add(_grid, 0, 0);
        _layout.Add(_logBox, 0, 0);
        _layout.Add(_btnAdd, 0, 0);
        _layout.Add(_btnClear, 0, 0);
        _layout.Add(_btnSettings, 0, 0);
        _layout.Add(_btnStartTest, 0, 0);
        _layout.Add(_btnExport, 0, 0);
        Content = _layout;

        SizeChanged += (_, _) => Relayout();
        Shown += (_, _) => Relayout();
    }

    private GridView BuildGrid()
    {
        var grid = new GridView
        {
            DataStore = _model.ProxyList,
            AllowMultipleSelection = false,
            ShowHeader = true,
        };
        grid.Columns.Add(new GridColumn
        {
            HeaderText = "IP",
            DataCell = new TextBoxCell { Binding = Binding.Property<Proxy, string>(p => p.Endpoint.Address.ToString()) },
            Width = 160,
        });
        grid.Columns.Add(new GridColumn
        {
            HeaderText = "Port",
            DataCell = new TextBoxCell { Binding = Binding.Property<Proxy, string>(p => p.Endpoint.Port.ToString()) },
            Width = 80,
        });
        grid.Columns.Add(new GridColumn
        {
            HeaderText = "Status",
            DataCell = new TextBoxCell { Binding = Binding.Property<Proxy, string>(p => p.Status) },
            Width = 120,
        });
        return grid;
    }

    private void Relayout()
    {
        var w = ClientSize.Width;
        var h = ClientSize.Height;
        if (w <= 0 || h <= 0) return;

        const int margin = 12;
        const int btnW = 75;
        const int btnH = 23;
        const int logH = 102;
        var rightX = w - btnW - margin;
        var contentW = rightX - margin - margin;

        _layout.Move(_grid, margin, margin);
        _grid.Size = new Size(Math.Max(100, contentW), Math.Max(50, h - margin * 3 - logH));

        _layout.Move(_logBox, margin, h - logH - margin);
        _logBox.Size = new Size(Math.Max(100, contentW), logH);

        _layout.Move(_btnAdd, rightX, margin);
        _layout.Move(_btnClear, rightX, margin + btnH + 6);
        _layout.Move(_btnSettings, rightX, margin + (btnH + 6) * 2);

        _layout.Move(_btnStartTest, rightX, h - margin - btnH * 2 - 6);
        _layout.Move(_btnExport, rightX, h - margin - btnH);
    }

    private async Task OnAddProxy()
    {
        var dlg = new AddProxyForm();
        var result = await dlg.ShowModalAsync(this);
        if (result is { Count: > 0 })
        {
            _model.AddRange(result);
            RefreshGrid();
            Log($"Added {result.Count} proxies");
        }
    }

    private async Task OnSettings()
    {
        var dlg = new SettingsForm(_settings);
        var result = await dlg.ShowModalAsync(this);
        if (result != null)
        {
            _settings = result;
            Log("Settings updated");
        }
    }

    private async Task StartTestAsync()
    {
        if (_model.ProxyList.Count == 0) { Log("No proxies to test"); return; }
        if (_cts != null) { Log("Test already running"); return; }

        _cts = new CancellationTokenSource();
        _progressForm = new TestProgressForm();
        _progressForm.Show();

        var snapshot = _model.ProxyList.ToArray();
        var total = snapshot.Length;
        var done = 0;
        Log($"Testing {total} proxies (parallelism={_settings.ParallelLimit}, timeout={_settings.ConnectionTimeout.TotalMilliseconds:0}ms)");

        try
        {
            await Parallel.ForEachAsync(
                snapshot,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = _settings.ParallelLimit,
                    CancellationToken = _cts.Token,
                },
                async (proxy, ct) =>
                {
                    await proxy.PerformTestAsync(_settings.ConnectionTimeout, _settings.UserAgent, ct);
                    var n = Interlocked.Increment(ref done);
                    Application.Instance.AsyncInvoke(() =>
                    {
                        var pct = (int)(n * 100.0 / total);
                        _progressForm?.SetProgress(pct);
                        RefreshGrid();
                    });
                });

            var online = snapshot.Count(p => p.Status == "online");
            Log($"Done. {online}/{total} online");
        }
        catch (OperationCanceledException)
        {
            Log("Test cancelled");
        }
        finally
        {
            _progressForm?.Close();
            _progressForm = null;
            _cts?.Dispose();
            _cts = null;
        }
    }

    private void RefreshGrid()
    {
        _grid.DataStore = null;
        _grid.DataStore = _model.ProxyList;
    }

    private void Export()
    {
        if (_model.ProxyList.Count == 0) { Log("Nothing to export"); return; }

        var dlg = new SaveFileDialog
        {
            Title = "Export proxies",
            Filters = { new FileFilter("Text", ".txt") },
            FileName = "proxies.txt",
        };
        if (dlg.ShowDialog(this) != DialogResult.Ok) return;

        var lines = _model.ProxyList.Select(p => $"{p.Endpoint.Address}:{p.Endpoint.Port} {p.Status}");
        File.WriteAllLines(dlg.FileName, lines);
        Log($"Exported to {dlg.FileName}");
    }

    private void Log(string message)
    {
        var stamp = DateTime.Now.ToString("HH:mm:ss");
        _logBox.Text = $"[{stamp}] {message}\n" + _logBox.Text;
    }
}
