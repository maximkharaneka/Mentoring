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
      <News>
        <article>
          Список книг на текущее время  <xsl:value-of select="user:curYear()"/>
        </article>
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
      </News>
  </xsl:template>

  <xsl:template name="catalog">
    <xsl:param name="genre_type"/>
    <library>
      <genre>
        <xsl:value-of select="$genre_type"/>
      </genre>
      <xsl:for-each select="cd:catalog/cd:book[cd:genre=$genre_type]">
        <news>
          <xsl:choose>
            <xsl:when test="cd:isbn">
              <link>
                http://my.safaribooksonline.com/<xsl:value-of select="cd:isbn"/>
              </link>
            </xsl:when>
          </xsl:choose>
          <author>
            <xsl:value-of select="cd:author"/>
          </author>
          <title>
            <xsl:value-of select="cd:title"/>
          </title>
          <publish_date>
            <xsl:value-of select="cd:publish_date"/>
          </publish_date>
          <registration_date>
            <xsl:value-of select="cd:registration_date"/>
          </registration_date>
        </news>
      </xsl:for-each>
    </library>
  </xsl:template>
</xsl:stylesheet>