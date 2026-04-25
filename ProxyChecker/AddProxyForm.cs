using Eto.Drawing;
using Eto.Forms;

namespace ProxyChecker;

public sealed class AddProxyForm : Dialog<IReadOnlyList<Proxy>?>
{
    private readonly TextArea _proxyTextBox;

    public AddProxyForm()
    {
        Title = "Add Proxy";
        ClientSize = new Size(486, 414);
        Resizable = false;

        _proxyTextBox = new TextArea
        {
            Size = new Size(462, 363),
        };

        var labelFormat = new Label
        {
            Text = "One IP and port divided by : or , per line",
        };

        var btnOk = new Button { Text = "Add", Size = new Size(75, 23) };
        btnOk.Click += (_, _) =>
        {
            Close(Parse());
        };

        var btnCancel = new Button { Text = "Storno", Size = new Size(75, 23) };
        btnCancel.Click += (_, _) => Close(null);

        var layout = new PixelLayout();
        layout.Add(_proxyTextBox, 12, 12);
        layout.Add(labelFormat, 12, 386);
        layout.Add(btnCancel, 318, 381);
        layout.Add(btnOk, 399, 381);
        Content = layout;

        DefaultButton = btnOk;
        AbortButton = btnCancel;
    }

    private IReadOnlyList<Proxy> Parse()
    {
        var list = new List<Proxy>();
        using var reader = new StringReader(_proxyTextBox.Text ?? string.Empty);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var p = Proxy.Parse(line);
            if (p != null) list.Add(p);
        }
        return list;
    }
}
