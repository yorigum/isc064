using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ISC064
{
    public class SitePlan : HtmlGenericControl
    {
        /// <summary>
        /// Made By WHINTZ
        /// 
        /// 
        /// control ini terdiri dari 2 lapisan
        ///     1. SVG Warna  => punya background gambar dasar + buat warna in status unit nya
        ///     2. SVG Link => ini link ,, 
        ///     
        /// 
        /// </summary>
        /// 
        HtmlGenericControl SVGWarna;
        HtmlGenericControl SVGLink;
        public SitePlan(float Width, float Height, string UrlPetaDasar, string UrlPetaTransparent)
        {
            TagName = "div";
            Attributes.Add("style", "position:relative;");

            SVGWarna = new HtmlGenericControl("svg");
            SVGWarna.Attributes.Add("style","width:"+ Width +"px;height:"+ Height + "px;background-image: url('" + UrlPetaDasar + "');background-repeat:no-repeat;position: absolute;");

            SVGLink = new HtmlGenericControl("svg");
            SVGLink.Attributes.Add("style", "width: " + Width + "px; height: " + Height + "px; background-image: url('" + UrlPetaTransparent + "');background-repeat:no-repeat;position: absolute;");

            Controls.Add(SVGWarna);
            Controls.Add(SVGLink);
        }

        public void Draw(string Coordinate, string Color, string Href)
        {
            var polygon = new HtmlGenericControl("polygon");

            // SVG WARNA
            polygon.Attributes.Add("points", Coordinate);
            polygon.Attributes.Add("style", "fill: " + Color + ";stroke:purple;stroke-width:0;");
            SVGWarna.Controls.Add(polygon);


            // SVG Link
            var a = new HtmlGenericControl("a");
            a.Attributes.Add("href", Href);
            polygon = new HtmlGenericControl("polygon");
            polygon.Attributes.Add("points", Coordinate);
            polygon.Attributes.Add("style", "fill: " + Color + ";stroke:purple;stroke-width:0;");
            polygon.Attributes.Add("fill-opacity", "0.5");
            a.Controls.Add(polygon);
            SVGLink.Controls.Add(a);
        }

        public void Draw(string Coordinate, string Color, string Href, string AttibuteName, string AttributeValue, string NoStock)
        {
            var polygon = new HtmlGenericControl("polygon");

            // SVG WARNA
            polygon.Attributes.Add("points", Coordinate);
            polygon.Attributes.Add("style", "fill: " + Color + ";stroke:purple;stroke-width:0;");
            polygon.Attributes.Add("data-stock", NoStock);
            SVGWarna.Controls.Add(polygon);


            // SVG Link
            var a = new HtmlGenericControl("a");
            a.Attributes.Add("href", Href);
            polygon = new HtmlGenericControl("polygon");
            polygon.Attributes.Add("points", Coordinate);
            polygon.Attributes.Add("style", "fill: " + Color + ";stroke:purple;stroke-width:0;");
            polygon.Attributes.Add("fill-opacity", "0.5");
            polygon.Attributes.Add("data-stock", NoStock);
            a.Controls.Add(polygon);
            a.Attributes.Add(AttibuteName, AttributeValue);
            SVGLink.Controls.Add(a);
        }
    }
}
