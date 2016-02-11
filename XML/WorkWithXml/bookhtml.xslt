<?xml version="1.0"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:cd="http://library.by/catalog"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:user="urn:my-scripts"
                 extension-element-prefixes="cd" >

  <msxsl:script implements-prefix='user' language='CSharp'>
    <![CDATA[
    public string curYear() {
    return System.DateTime.Now.ToString();
    }]]>
  </msxsl:script>

  <xsl:template match="/">
    <html>
      <body>
        <h2>
          Текущие фонды по жанрам  <xsl:value-of select="user:curYear()"/>
        </h2>
        <xsl:call-template name="catalog">
          <xsl:with-param name="genre_type">Fantasy</xsl:with-param>
        </xsl:call-template>
        <xsl:call-template name="catalog">
          <xsl:with-param name="genre_type">Computer</xsl:with-param>
        </xsl:call-template>
        <xsl:call-template name="catalog">
          <xsl:with-param name="genre_type">Romance</xsl:with-param>
        </xsl:call-template>
        <xsl:call-template name="catalog">
          <xsl:with-param name="genre_type">Horror</xsl:with-param>
        </xsl:call-template>
        <xsl:call-template name="catalog">
          <xsl:with-param name="genre_type">Science Fiction</xsl:with-param>
        </xsl:call-template>
        <h2>
          Count of books in the library: <xsl:value-of  select="count(cd:catalog/cd:book)"/>
        </h2>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="catalog">
    <xsl:param name="genre_type"/>
    <table border="1">
      <caption>
        <xsl:value-of select="$genre_type"/>
      </caption>
      <tr bgcolor="#9acd32">
        <th>Автор</th>
        <th>Название</th>
        <th>Дата издания</th>
        <th>Дата регистрации</th>
      </tr>
      <xsl:for-each select="cd:catalog/cd:book[cd:genre=$genre_type]">
        <tr>
          <td>
            <xsl:value-of select="cd:author"/>
          </td>
          <td>
            <xsl:value-of select="cd:title"/>
          </td>
          <td>
            <xsl:value-of select="cd:publish_date"/>
          </td>
          <td>
            <xsl:value-of select="cd:registration_date"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>
    <p>
      Count of books with genre <xsl:value-of select="$genre_type"/> : <xsl:value-of  select="count(cd:catalog/cd:book[cd:genre=$genre_type])"/>
    </p>
  </xsl:template>
</xsl:stylesheet>