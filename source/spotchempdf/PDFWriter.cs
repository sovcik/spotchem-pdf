using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using log4net;

namespace spotchempdf
{
    enum Anchor
    {
        TopLeft,
        TopCenter,
        TopRight,
        MidLeft,
        MidCenter,
        MidRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    class PDFWriter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PDFWriter));

        XGraphics graph;
        PdfDocument pdf;
        XBrush brush = XBrushes.Black;
        XFont font = new XFont("Verdana", 11, XFontStyle.Regular);
        ReadingRanges ranges = new ReadingRanges();

        int[] tc = { 50, 120, 230, 330, 330, 550 };
        int tRowHeight = 17;
        int tTextSize = 11;

        public PDFWriter(ReadingRanges ranges)
        {
            this.ranges = ranges;
        }

        public string savePDF(Reading rd, string pdfFileName, bool OpenAfterSave, Provider provider, RangeType rangeType)
        {
            XPen pen = XPens.Black;

            log.Debug("Saving reading into PDF. Reading=" + rd.GetTitle() + " PDF=" + pdfFileName);

            int topMargin = 50;
            int leftMargin = 50;
            int headerSize = 140;

            pdf = new PdfDocument();
            pdf.Info.Title = "SpotchemPDF";
            PdfPage pdfPage = pdf.AddPage();
            graph = XGraphics.FromPdfPage(pdfPage);

            double x = pdfPage.Width.Point - 230;
            double y = topMargin;

            // address
            graph.DrawString(provider.name, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.address1, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.address2, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.address3, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.address4, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.contact1, font, brush, x, y);
            y += 17;
            graph.DrawString(provider.contact2, font, brush, x, y);
            y += 17;

            // date & time
            x = leftMargin;
            y = topMargin;
            graph.DrawString("Dátum: " + rd.date.ToShortDateString()+"   "+rd.date.ToShortTimeString(), font, brush, x, y);

            // client/animal details
            y += 2*17;
            graph.DrawString("Id: " + rd.id, font, brush, x, y);
            y += 17;
            graph.DrawString("Klient: (" + rd.clientId + ") " + rd.clientName, font, brush, x, y);
            y += 17;
            graph.DrawString("Typ zvieraťa: " + rd.animalType, font, brush, x, y);
            y += 17;
            graph.DrawString("Zviera: "+rd.animalName+"  vek="+rd.animalAge, font, brush, x, y);


            // table column names
            y = topMargin + headerSize;
            graph.DrawString("Test", font, brush, tc[0], y);
            graph.DrawString("Výsledok", font, brush, tc[1], y);
            graph.DrawString("Interval", font, brush, tc[2], y);

            int mark = (tc[5] - tc[4]) / 3;
            graph.DrawString("Nízky", font, brush, new XRect(tc[4], y - tRowHeight, mark, tRowHeight), XStringFormats.BottomCenter);
            graph.DrawString("Normálny", font, brush, new XRect(tc[4]+mark, y - tRowHeight, mark, tRowHeight), XStringFormats.BottomCenter);
            graph.DrawString("Vysoký", font, brush,  new XRect(tc[5] - mark, y - tRowHeight, mark, tRowHeight), XStringFormats.BottomCenter);


            // horizontal line
            y += 5;
            graph.DrawLine(pen, tc[0], y, tc[4]-10, y);

            y += tRowHeight;

            IEnumerator<KeyValuePair<string,ReadingItem>> ie = rd.items.GetEnumerator();

            int i = 0;
            while (ie.MoveNext()) { 
                Range r = new Range();
                rangeType.ranges.TryGetValue(ie.Current.Value.name, out r);
                if (r == null) r = new Range(ie.Current.Value.value, ie.Current.Value.value,"x");
                if (ie.Current.Value.error == null || ie.Current.Value.error.Length == 0)
                    writeTableRow(false, y + i * tRowHeight, ie.Current.Value.name, ie.Current.Value.value.ToString()+" "+ie.Current.Value.unit, r, ie.Current.Value.value);
                else
                    writeTableRow(true, y + i * tRowHeight, ie.Current.Value.name, ie.Current.Value.error, r, 0);
                i++;
            }


            //graph.DrawString(s, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

            int c = 1;
            if (File.Exists(pdfFileName + ".pdf"))
            {
                log.Debug("File " + pdfFileName + ".pdf already exists. Adding version number.");
                while (File.Exists(pdfFileName + "(" + c + ").pdf")) c++;
                pdfFileName += "(" + c + ")";
            }

            pdfFileName += ".pdf";
            pdf.Save(pdfFileName);
            log.Info("PDF saved to " + pdfFileName);

            if (OpenAfterSave)
            {
                log.Debug("Opening PDF Viewer for " + pdfFileName);
                Process.Start(pdfFileName);
            }

            return pdfFileName;
        }

        private void writeTableRow(bool error, double y, string testName, string testResult, Range range, float result)
        {
            //string resWord = "";
            XBrush resBrush = brush;
            XPen resPen = XPens.Black;
            

            if (result < range.min) {
                //resWord = "Nízky";
                resBrush = XBrushes.Blue;
                resPen = XPens.Blue;
            }

            if (result > range.max) {
                //resWord = "Vysoký";
                resBrush = XBrushes.Red;
                resPen = XPens.Red;
            }

            graph.DrawString(testName, font, resBrush, tc[0], y);
            graph.DrawString(testResult, font, resBrush, tc[1], y);

            if (!error && range.max != range.min)
            {
                int mark = (tc[5] - tc[4]) / 3;
                float resScaleMax = range.max + (range.max - range.min);
                float resScaleMin = range.min - (range.max - range.min);

                float resScalePos = result;
                if (result < resScaleMin) resScalePos = resScaleMin;
                if (result > resScaleMax) resScalePos = resScaleMax;
                resScalePos = (resScalePos - resScaleMin) / (resScaleMax - resScaleMin);

                // print range
                graph.DrawString(range.min+" - "+range.max, font, resBrush, tc[2], y);

                // print word explanation of value within range
                //graph.DrawString(resWord, font, resBrush, tc[3], y);

                // draw horizontal axis
                graph.DrawLine(XPens.Black, tc[4], y - tTextSize/2, tc[5], y - tTextSize / 2);

                // draw vertical markers
                graph.DrawLine(XPens.Black, tc[4] + mark, y - tTextSize, tc[4] + mark, y);
                graph.DrawLine(XPens.Black, tc[5] - mark, y - tTextSize, tc[5] - mark, y);

                // drawm marker for the value
                mark = tc[4]+(int)((tc[5] - tc[4]) * resScalePos);
                graph.DrawRectangle(resBrush, mark, y - tTextSize * 0.8, 4, tTextSize * 0.6);
            }
        }


    }
}
