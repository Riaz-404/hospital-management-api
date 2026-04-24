using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RootController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var baseUrl = GetBaseUrl();
        var html = BuildHtmlPage(baseUrl);
        return Content(html, "text/html");
    }

    [HttpGet("info")]
    public IActionResult Info()
    {
        var baseUrl = GetBaseUrl();

        return Ok(new
        {
            application = "Hospital Management System API",
            version = "1.0.0",
            status = "running",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
            baseUrl,
            routes = new
            {
                root        = $"{baseUrl}/",
                info        = $"{baseUrl}/info",
                health      = $"{baseUrl}/health",
                swagger     = $"{baseUrl}/swagger",
                patients    = $"{baseUrl}/api/patients",
                doctors     = $"{baseUrl}/api/doctors",
                appointments = $"{baseUrl}/api/appointments",
                consultations = $"{baseUrl}/api/consultations",
                tests       = $"{baseUrl}/api/tests",
                payments    = $"{baseUrl}/api/payments",
                reports     = $"{baseUrl}/api/reports",
            },
            external = new
            {
                github = _configuration["AppSettings:GitHubUrl"] ?? "",
                documentation = $"{baseUrl}/swagger",
            },
            timestamp = DateTime.UtcNow
        });
    }

    // ──────────────────────────────────────────────────────────────
    // Dynamically build the base URL from the incoming request
    // Works on localhost, Render, Railway, Azure — anywhere.
    // ──────────────────────────────────────────────────────────────
    private string GetBaseUrl()
    {
        var request = HttpContext.Request;

        // Render (and most reverse proxies) forward the original
        // scheme and host in these standard headers.
        var forwardedProto = request.Headers["X-Forwarded-Proto"].FirstOrDefault();
        var forwardedHost  = request.Headers["X-Forwarded-Host"].FirstOrDefault();

        var scheme = forwardedProto ?? request.Scheme;
        var host   = forwardedHost  ?? request.Host.ToString();

        return $"{scheme}://{host}";
    }

    private string BuildHtmlPage(string baseUrl)
    {
        var env       = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var github    = _configuration["AppSettings:GitHubUrl"] ?? "https://github.com/yourusername/HospitalManagement";
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + " UTC";

        var routes = new[]
        {
            new { Icon = "💚", Label = "Health Check",    Path = "/health",            Description = "Service health status" },
            // new { Icon = "📚", Label = "API Docs",        Path = "/swagger",           Description = "Interactive Swagger documentation" },
            // new { Icon = "ℹ️",  Label = "API Info (JSON)", Path = "/info",             Description = "Machine-readable service info" },
            new { Icon = "👥", Label = "Patients",        Path = "/api/patients",      Description = "Patient management endpoints" },
            new { Icon = "👨‍⚕️", Label = "Doctors",         Path = "/api/doctors",       Description = "Doctor management endpoints" },
            new { Icon = "📅", Label = "Appointments",    Path = "/api/appointments",  Description = "Appointment scheduling endpoints" },
            new { Icon = "🏥", Label = "Consultations",   Path = "/api/consultations", Description = "Medical consultation endpoints" },
            new { Icon = "🧪", Label = "Tests",           Path = "/api/tests",         Description = "Medical test endpoints" },
            new { Icon = "💰", Label = "Payments",        Path = "/api/payments",      Description = "Payment management endpoints" },
            new { Icon = "📋", Label = "Reports",         Path = "/api/reports",       Description = "Medical report endpoints" },
        };

        var routeRows = string.Join("\n", routes.Select(r => $@"
            <a href=""{baseUrl}{r.Path}"" class=""route-card"" target=""_blank"">
                <span class=""route-icon"">{r.Icon}</span>
                <div class=""route-info"">
                    <span class=""route-label"">{r.Label}</span>
                    <span class=""route-desc"">{r.Description}</span>
                </div>
                <span class=""route-path"">{r.Path}</span>
            </a>"));

        return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Hospital Management API</title>
    <link rel=""preconnect"" href=""https://fonts.googleapis.com"" />
    <link href=""https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700;800;900&family=JetBrains+Mono:wght@400;500&display=swap"" rel=""stylesheet"" />
    <style>
        *, *::before, *::after {{ box-sizing: border-box; margin: 0; padding: 0; }}

        :root {{
            --bg:        #0a0f1e;
            --surface:   #111827;
            --border:    #1e2d45;
            --accent:    #00d4ff;
            --accent2:   #7c3aed;
            --green:     #10b981;
            --text:      #e2e8f0;
            --muted:     #64748b;
            --card-bg:   #0d1525;
        }}

        body {{
            background: var(--bg);
            color: var(--text);
            font-family: 'Syne', sans-serif;
            min-height: 100vh;
            overflow-x: hidden;
        }}

        /* Animated grid background */
        body::before {{
            content: '';
            position: fixed;
            inset: 0;
            background-image:
                linear-gradient(var(--border) 1px, transparent 1px),
                linear-gradient(90deg, var(--border) 1px, transparent 1px);
            background-size: 48px 48px;
            opacity: 0.4;
            z-index: 0;
        }}

        /* Glowing orbs */
        body::after {{
            content: '';
            position: fixed;
            top: -200px;
            left: -200px;
            width: 600px;
            height: 600px;
            background: radial-gradient(circle, rgba(0,212,255,0.06) 0%, transparent 70%);
            z-index: 0;
            pointer-events: none;
        }}

        .orb2 {{
            position: fixed;
            bottom: -200px;
            right: -200px;
            width: 600px;
            height: 600px;
            background: radial-gradient(circle, rgba(124,58,237,0.08) 0%, transparent 70%);
            z-index: 0;
            pointer-events: none;
        }}

        .container {{
            position: relative;
            z-index: 1;
            max-width: 900px;
            margin: 0 auto;
            padding: 60px 24px 80px;
        }}

        /* ── Header ── */
        .header {{
            text-align: center;
            margin-bottom: 56px;
            animation: fadeUp 0.6s ease both;
        }}

        .badge {{
            display: inline-flex;
            align-items: center;
            gap: 6px;
            background: rgba(16,185,129,0.12);
            border: 1px solid rgba(16,185,129,0.3);
            color: var(--green);
            font-family: 'JetBrains Mono', monospace;
            font-size: 12px;
            padding: 5px 14px;
            border-radius: 100px;
            margin-bottom: 24px;
            letter-spacing: 0.05em;
        }}

        .badge::before {{
            content: '';
            width: 7px;
            height: 7px;
            background: var(--green);
            border-radius: 50%;
            animation: pulse 2s infinite;
        }}

        @keyframes pulse {{
            0%, 100% {{ opacity: 1; transform: scale(1); }}
            50%        {{ opacity: 0.4; transform: scale(0.8); }}
        }}

        h1 {{
            font-size: clamp(2rem, 5vw, 3.2rem);
            font-weight: 800;
            line-height: 1.1;
            letter-spacing: -0.03em;
            margin-bottom: 14px;
        }}

        h1 span {{
            background: linear-gradient(135deg, var(--accent), var(--accent2));
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            background-clip: text;
        }}

        .subtitle {{
            color: var(--muted);
            font-size: 1rem;
            font-weight: 400;
            max-width: 480px;
            margin: 0 auto;
            line-height: 1.6;
        }}

        /* ── Base URL card ── */
        .url-card {{
            background: var(--card-bg);
            border: 1px solid var(--border);
            border-left: 3px solid var(--accent);
            border-radius: 12px;
            padding: 20px 24px;
            margin-bottom: 40px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 16px;
            flex-wrap: wrap;
            animation: fadeUp 0.6s 0.1s ease both;
        }}

        .url-label {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 11px;
            color: var(--accent);
            letter-spacing: 0.1em;
            text-transform: uppercase;
            margin-bottom: 6px;
        }}

        .url-value {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 1rem;
            color: var(--text);
            word-break: break-all;
        }}

        .meta-chips {{
            display: flex;
            gap: 8px;
            flex-wrap: wrap;
            flex-shrink: 0;
        }}

        .chip {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 11px;
            padding: 4px 10px;
            border-radius: 6px;
            border: 1px solid var(--border);
            color: var(--muted);
        }}

        .chip.env {{ color: #f59e0b; border-color: rgba(245,158,11,0.3); background: rgba(245,158,11,0.05); }}
        .chip.ver {{ color: var(--accent); border-color: rgba(0,212,255,0.3); background: rgba(0,212,255,0.05); }}

        /* ── Section title ── */
        .section-title {{
            font-size: 11px;
            font-family: 'JetBrains Mono', monospace;
            letter-spacing: 0.15em;
            text-transform: uppercase;
            color: var(--muted);
            margin-bottom: 16px;
        }}

        /* ── Route cards ── */
        .routes-grid {{
            display: flex;
            flex-direction: column;
            gap: 8px;
            margin-bottom: 40px;
            animation: fadeUp 0.6s 0.2s ease both;
        }}

        .route-card {{
            display: flex;
            align-items: center;
            gap: 16px;
            background: var(--card-bg);
            border: 1px solid var(--border);
            border-radius: 10px;
            padding: 14px 18px;
            text-decoration: none;
            color: var(--text);
            transition: border-color 0.2s, background 0.2s, transform 0.15s;
            cursor: pointer;
        }}

        .route-card:hover {{
            border-color: var(--accent);
            background: rgba(0,212,255,0.04);
            transform: translateX(4px);
        }}

        .route-icon {{
            font-size: 1.2rem;
            flex-shrink: 0;
            width: 28px;
            text-align: center;
        }}

        .route-info {{
            flex: 1;
            min-width: 0;
        }}

        .route-label {{
            display: block;
            font-weight: 600;
            font-size: 0.9rem;
            color: var(--text);
            margin-bottom: 2px;
        }}

        .route-desc {{
            display: block;
            font-size: 0.78rem;
            color: var(--muted);
        }}

        .route-path {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 0.75rem;
            color: var(--accent);
            flex-shrink: 0;
            background: rgba(0,212,255,0.07);
            padding: 3px 10px;
            border-radius: 6px;
            border: 1px solid rgba(0,212,255,0.15);
        }}

        /* ── External links row ── */
        .ext-row {{
            display: flex;
            gap: 12px;
            flex-wrap: wrap;
            margin-bottom: 40px;
            animation: fadeUp 0.6s 0.3s ease both;
        }}

        .ext-btn {{
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 11px 20px;
            border-radius: 10px;
            text-decoration: none;
            font-size: 0.875rem;
            font-weight: 600;
            border: 1px solid var(--border);
            color: var(--text);
            background: var(--card-bg);
            transition: border-color 0.2s, color 0.2s, background 0.2s;
        }}

        .ext-btn:hover {{
            border-color: var(--accent2);
            color: #a78bfa;
            background: rgba(124,58,237,0.06);
        }}

        .ext-btn svg {{ flex-shrink: 0; }}

        /* ── Footer ── */
        .footer {{
            border-top: 1px solid var(--border);
            padding-top: 24px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 12px;
            animation: fadeUp 0.6s 0.4s ease both;
        }}

        .footer-left {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 12px;
            color: var(--muted);
        }}

        .footer-ts {{
            font-family: 'JetBrains Mono', monospace;
            font-size: 11px;
            color: var(--muted);
            opacity: 0.6;
        }}

        @keyframes fadeUp {{
            from {{ opacity: 0; transform: translateY(16px); }}
            to   {{ opacity: 1; transform: translateY(0); }}
        }}

        @media (max-width: 600px) {{
            .route-path {{ display: none; }}
            .url-card {{ flex-direction: column; align-items: flex-start; }}
        }}
    </style>
</head>
<body>
    <div class=""orb2""></div>
    <div class=""container"">

        <header class=""header"">
            <div class=""badge"">RUNNING · v1.0.0</div>
            <h1>Hospital Management<br/><span>System API</span></h1>
            <p class=""subtitle"">
                A professional-grade REST API for managing hospital operations —
                patients, doctors, appointments, consultations, tests, payments, and reports.
            </p>
        </header>

        <!-- Base URL -->
        <div class=""url-card"">
            <div>
                <div class=""url-label"">⚡ Base URL (current host)</div>
                <div class=""url-value"">{baseUrl}</div>
            </div>
            <div class=""meta-chips"">
                <span class=""chip env"">{env}</span>
                <span class=""chip ver"">v1.0.0</span>
            </div>
        </div>

        <!-- Routes -->
        <p class=""section-title"">Available Routes</p>
        <div class=""routes-grid"">
            {routeRows}
        </div>

        <!-- External links -->
        <p class=""section-title"">External Links</p>
        <div class=""ext-row"">
            <a href=""{github}"" class=""ext-btn"" target=""_blank"">
                <svg width=""16"" height=""16"" fill=""currentColor"" viewBox=""0 0 24 24"">
                    <path d=""M12 0C5.37 0 0 5.37 0 12c0 5.3 3.438 9.8 8.205 11.385.6.113.82-.258.82-.577 0-.285-.01-1.04-.015-2.04-3.338.724-4.042-1.61-4.042-1.61-.546-1.387-1.333-1.756-1.333-1.756-1.09-.745.083-.729.083-.729 1.205.084 1.84 1.236 1.84 1.236 1.07 1.835 2.809 1.305 3.495.998.108-.776.418-1.305.76-1.605-2.665-.3-5.466-1.332-5.466-5.93 0-1.31.468-2.38 1.235-3.22-.124-.303-.535-1.524.117-3.176 0 0 1.008-.322 3.3 1.23A11.51 11.51 0 0 1 12 5.803c1.02.005 2.047.138 3.006.404 2.29-1.552 3.297-1.23 3.297-1.23.653 1.653.242 2.874.118 3.176.77.84 1.233 1.91 1.233 3.22 0 4.61-2.804 5.625-5.475 5.92.43.372.823 1.102.823 2.222 0 1.606-.015 2.896-.015 3.286 0 .322.216.694.825.576C20.565 21.795 24 17.298 24 12c0-6.63-5.37-12-12-12z""/>
                </svg>
                GitHub Repository
            </a>
        </div>

        <!-- Footer -->
        <footer class=""footer"">
            <div class=""footer-left"">
                Hospital Management System API &nbsp;·&nbsp; ASP.NET Core 10 &nbsp;·&nbsp; Clean Architecture
            </div>
            <div class=""footer-ts"">Generated at {timestamp}</div>
        </footer>

    </div>
</body>
</html>";
    }
}