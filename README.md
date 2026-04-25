ProxyChecker
=============

Simple cross-platform application for batch checking whether a proxy server is working.

Originally a WinForms app from 2013. Modernized to .NET 10 with [Eto.Forms](https://github.com/picoe/Eto) for cross-platform UI (macOS / Windows / Linux), preserving the original layout.

## Build & run

```sh
cd ProxyChecker
dotnet build
dotnet run
```

On macOS, `dotnet build` produces a `ProxyChecker.app` bundle under `bin/Debug/net10.0/<rid>/`.
