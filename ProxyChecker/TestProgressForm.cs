using Eto.Drawing;
using Eto.Forms;

namespace ProxyChecker;

public sealed class TestProgressForm : Form
{
    private readonly ProgressBar _progressBar;
    private readonly Label _progressLabel;

    public TestProgressForm()
    {
        Title = "Test Progress";
        ClientSize = new Size(438, 80);
        Resizable = false;
        Maximizable = false;
        Minimizable = false;

        _progressBar = new ProgressBar
        {
            MinValue = 0,
            MaxValue = 100,
            Size = new Size(414, 18),
        };

        _progressLabel = new Label
        {
            Text = "0%",
            TextAlignment = TextAlignment.Center,
            Width = 414,
        };

        var layout = new PixelLayout();
        layout.Add(_progressLabel, 12, 22);
        layout.Add(_progressBar, 12, 50);
        Content = layout;
    }

    public void SetProgress(int progress)
    {
        _progressBar.Value = progress;
        _progressLabel.Text = $"{progress}%";
    }
}
