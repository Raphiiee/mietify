# AOT oder JIT
---
## AOT
Der Vorteil von nativem AOT ist vor allem für Arbeitslasten mit einer hohen Anzahl von bereitgestellten Instanzen, wie Cloud-Infrastrukturen und Hyper-Scale-Dienste, von Bedeutung. Es wird derzeit nicht von ASP.NET Core unterstützt, sondern nur von Konsolenanwendungen. https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/

Beschränkungen:

Native AOT-Anwendungen sind mit einigen grundlegenden Einschränkungen und Kompatibilitätsproblemen verbunden. Zu den wichtigsten Einschränkungen gehören:
   - Kein dynamisches Laden (z. B. Assembly.LoadFile)
   - Keine Codegenerierung zur Laufzeit (z. B. System.Reflection.Emit)
   - Kein C++/CLI
   - Kein integriertes COM (gilt nur für Windows)
   - Erfordert Trimming, was Einschränkungen mit sich bringt
   - Erfordert die Kompilierung in eine einzige Datei, was zu bekannten Inkompatibilitäten führt
   - Apps enthalten erforderliche Laufzeitbibliotheken (genau wie eigenständige Apps, was ihre Größe im Vergleich zu Framework-abhängigen Apps erhöht)

=> Also AOT Anwendungen falls Startup Speed, Low Memory Footprint & Small Packaging wichtig sind. Wie bei Clients von Anwendungen wie Teams, Zoom, etc.

## JIT
