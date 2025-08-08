<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:trx="http://microsoft.com/schemas/VisualStudio/TeamTest/2010"
  exclude-result-prefixes="trx">
  <xsl:output method="html" encoding="UTF-8" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <!-- indexar definiciones por id -->
  <xsl:key name="defById" match="/trx:TestRun/trx:TestDefinitions/trx:UnitTest" use="@id"/>

  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8"/>
        <title>Reporte de Pruebas (TRX)</title>
        <style>
          body{font-family:Segoe UI,Arial,sans-serif;background:#0f1115;color:#e6e6e6;margin:20px}
          h1{margin:0 0 10px 0}
          .summary{display:flex;gap:16px;margin:12px 0 20px;flex-wrap:wrap}
          .card{background:#1a1f2b;padding:12px 14px;border-radius:12px;min-width:180px}
          .ok{color:#00d084;font-weight:600}
          .fail{color:#ff5c5c;font-weight:600}
          table{width:100%;border-collapse:collapse;background:#121622;border-radius:12px;overflow:hidden}
          th,td{padding:10px 12px;border-bottom:1px solid #22283a}
          th{text-align:left;background:#1a1f2b}
          tr:hover{background:#161b28}
          .mono{font-family:Consolas,monospace;font-size:12px;white-space:pre-wrap}
        </style>
      </head>
      <body>
        <h1>Reporte de Pruebas (TRX)</h1>

        <xsl:variable name="results" select="/trx:TestRun/trx:Results/trx:UnitTestResult"/>
        <xsl:variable name="total"   select="count($results)"/>
        <xsl:variable name="passed"  select="count($results[@outcome='Passed'])"/>
        <xsl:variable name="failed"  select="count($results[@outcome='Failed'])"/>
        <xsl:variable name="skipped" select="count($results[@outcome='NotExecuted'])"/>

        <div class="summary">
          <div class="card">Total: <b><xsl:value-of select="$total"/></b></div>
          <div class="card">Pasadas: <b class="ok"><xsl:value-of select="$passed"/></b></div>
          <div class="card">Fallidas: <b class="fail"><xsl:value-of select="$failed"/></b></div>
          <div class="card">Saltadas: <b><xsl:value-of select="$skipped"/></b></div>
        </div>

        <table>
          <thead>
            <tr>
              <th>Prueba</th>
              <th>Clase</th>
              <th>Resultado</th>
              <th>Duración (s)</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="$results">
              <xsl:variable name="def" select="key('defById', @testId)"/>
              <tr>
                <td><xsl:value-of select="$def/@name"/></td>
                <td><xsl:value-of select="$def/trx:TestMethod/@className"/></td>
                <td>
                  <xsl:choose>
                    <xsl:when test="@outcome='Passed'"><span class="ok">PASSED</span></xsl:when>
                    <xsl:when test="@outcome='Failed'"><span class="fail">FAILED</span></xsl:when>
                    <xsl:otherwise>SKIPPED</xsl:otherwise>
                  </xsl:choose>
                </td>
                <td><xsl:value-of select="@duration"/></td>
              </tr>

              <xsl:if test="trx:Output/trx:StdOut or trx:Output/trx:ErrorInfo">
                <tr>
                  <td colspan="4" class="mono">
                    <xsl:if test="trx:Output/trx:StdOut">
                      <b>Output:</b>
                      <xsl:value-of select="trx:Output/trx:StdOut"/>
                    </xsl:if>
                    <xsl:if test="trx:Output/trx:ErrorInfo">
                      <b>Mensaje:</b> <xsl:value-of select="trx:Output/trx:ErrorInfo/trx:Message"/><br/>
                      <b>Stack:</b> <xsl:value-of select="trx:Output/trx:ErrorInfo/trx:StackTrace"/>
                    </xsl:if>
                  </td>
                </tr>
              </xsl:if>
            </xsl:for-each>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
