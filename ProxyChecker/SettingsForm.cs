using Eto.Drawing;
using Eto.Forms;

namespace ProxyChecker;

public sealed class SettingsForm : Dialog<Settings?>
{
    private readonly TextBox _timeoutBox;
    private readonly TextBox _parallelBox;
    private readonly TextBox _userAgentBox;
    private readonly Settings _original;

    public SettingsForm(Settings current)
    {
        _original = current;
        Title = "Settings";
        ClientSize = new Size(510, 124);
        Resizable = false;

        _timeoutBox = new TextBox { Size = new Size(357, 20), Text = current.ConnectionTimeout.TotalMilliseconds.ToString("0") };
        _parallelBox = new TextBox { Size = new Size(357, 20), Text = current.ParallelLimit.ToString() };
        _userAgentBox = new TextBox { Size = new Size(357, 20), Text = current.UserAgent };

        var btnOk = new Button { Text = "OK", Size = new Size(75, 23) };
        btnOk.Click += (_, _) => Close(BuildSettings());

        var btnCancel = new Button { Text = "Storno", Size = new Size(75, 23) };
        btnCancel.Click += (_, _) => Close(null);

        var btnRestore = new Button { Text = "Restore defaults", Size = new Size(101, 23) };
        btnRestore.Click += (_, _) =>
        {
            var d = new Settings();
            _timeoutBox.Text = d.ConnectionTimeout.TotalMilliseconds.ToString("0");
            _parallelBox.Text = d.ParallelLimit.ToString();
            _userAgentBox.Text = d.UserAgent;
        };

        var layout = new PixelLayout();
        layout.Add(new Label { Text = "Connection timeout (ms):" }, 12, 9);
        layout.Add(_timeoutBox, 141, 6);
        layout.Add(new Label { Text = "Parallel requests limit:" }, 28, 35);
        layout.Add(_parallelBox, 141, 32);
        layout.Add(new Label { Text = "User agent:" }, 73, 61);
        layout.Add(_userAgentBox, 141, 58);
        layout.Add(btnRestore, 206, 89);
        layout.Add(btnCancel, 341, 89);
        layout.Add(btnOk, 422, 89);
        Content = layout;

        DefaultButton = btnOk;
        AbortButton = btnCancel;
    }

    private Settings BuildSettings()
    {
        var ms = int.TryParse(_timeoutBox.Text, out var t) ? t : (int)_original.ConnectionTimeout.TotalMilliseconds;
        var par = int.TryParse(_parallelBox.Text, out var p) && p > 0 ? p : _original.ParallelLimit;
        var ua = string.IsNullOrWhiteSpace(_userAgentBox.Text) ? _original.UserAgent : _userAgentBox.Text;
        return new Settings
        {
            ConnectionTimeout = TimeSpan.FromMilliseconds(ms),
            ParallelLimit = par,
            UserAgent = ua,
        };
    }
}
