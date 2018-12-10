using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Drawing.Printing;
using System.Resources;
using OkgoPrinter.Models;
using System.Text.RegularExpressions;

namespace OkgoPrinter.Helper
{
    public static class PrintHelper
    {
        #region Font
        //private static string fontName = "Dotrice Condensed";
        private static string fontName = FontFamily.GenericSansSerif.Name + " Condensed";

        private static Font font20 = new Font(fontName, 20, FontStyle.Bold, GraphicsUnit.Pixel);
        private static Font font10 = new Font(fontName, 10, FontStyle.Regular, GraphicsUnit.Pixel);
        private static Font font12 = new Font(fontName, 12, FontStyle.Regular, GraphicsUnit.Pixel);
        private static Font font12B = new Font(fontName, 12, FontStyle.Bold, GraphicsUnit.Pixel);
        private static Font font11B = new Font(fontName, 11, FontStyle.Bold, GraphicsUnit.Pixel);

        static Font font18 = new Font(fontName, 18, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font font18B = new Font(fontName, 18, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font font22B = new Font(fontName, 22, FontStyle.Bold, GraphicsUnit.Pixel);
        static Font font16 = new Font(fontName, 16, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font font16B = new Font(fontName, 16, FontStyle.Bold, GraphicsUnit.Pixel);
        private static Font font14 = new Font(fontName, 14, FontStyle.Regular, GraphicsUnit.Pixel);
        static Font font14B = new Font(fontName, 14, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion

        #region String Format
        static StringFormat stringFormatCenter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };
        static StringFormat stringFormatLeft = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        static StringFormat stringFormatRight = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Near
        };
        #endregion

        #region PrintHelper
        private static readonly ResourceManager _resourceManager;
        #endregion

        public static void PrintMenu(VMPrinter vm)
        {
            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = vm.PrinterName;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                    e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    int yHeader = 10;
                    int maxRight = 250;
                    Rectangle headRect = new Rectangle(0, yHeader, maxRight, 600);
                    #region header
                        e1.Graphics.DrawString(vm.transaction.merchant.name.ToUpper(), font14B, Brushes.Black, headRect, stringFormatCenter);
                        yHeader += 15;

                        // if(vm.transaction.user.fullname!=null && vm.transaction.payment!="CASHIER"){
                            e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                            yHeader += 15;
                            e1.Graphics.DrawString("Customer:"+vm.transaction.user.fullname, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 25), stringFormatLeft);
                            yHeader += 20;
                            e1.Graphics.DrawString("Telp:"+vm.transaction.user.phone, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 25), stringFormatLeft);
                            yHeader += 20;
                        // }

                    #endregion

                    #region divider
                        e1.Graphics.DrawString("===================================================================================================================================", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                        yHeader += 10;
                    #endregion

                    #region container
                        e1.Graphics.DrawString("Table "+vm.transaction.tableNo, font22B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 25), stringFormatCenter);
                        yHeader += 35;
                        var now = DateTime.Now;
                        string day = now.Day<10?"0"+now.Day:now.Day.ToString();
                        string month = now.Month<10?"0"+now.Month:now.Month.ToString();
                        string year = now.Year<10?"0"+now.Year:now.Year.ToString();
                        string hour = now.Hour<10?"0"+now.Hour:now.Hour.ToString();
                        string minute = now.Minute<10?"0"+now.Minute:now.Minute.ToString();
                        string second = now.Second<10?"0"+now.Second:now.Second.ToString();
                        e1.Graphics.DrawString("Date : " +day +"-" + month+"-" + year +":"+hour + "." + minute+ "." + second, font11B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        // e1.Graphics.DrawString(day +"-" + month+"-" + year +":"+hour + "." + minute+ "." + second, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                        yHeader += 25;
                        foreach(var i in vm.transaction.listTransactionDetail){
                            e1.Graphics.DrawString(i.qty+" x "+i.listing.name.ToUpper(), font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 20), stringFormatLeft);
                            yHeader += 23;
                            if(i.remark.Length>0){
                                var numberLineBreaks = Regex.Matches(i.remark, @"\n").Count;
                                string[] remarks = i.remark.Split("\n");
                                e1.Graphics.DrawString("Remark : ", font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                                    yHeader += 15;
                                for(int k=0; k<remarks.Length; k++){
                                    e1.Graphics.DrawString(" * "+remarks[k], font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                                    yHeader += 18;
                                } 
                                if(numberLineBreaks<1){
                                    yHeader += 15;
                                }
                            }
                            e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                            yHeader += 15;
                        }
                        yHeader += 10;
                    #endregion
            };

            try
            {
                p.Print();

            }
            catch (Exception ex)
            {
            }
        }
        public static void PrintServices(VMPrinter vm)
        {
            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = vm.PrinterName;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                int yHeader = 0;
                int maxRight = 250;
                Rectangle headRect = new Rectangle(0, yHeader, maxRight, 500);
                #region header
                    e1.Graphics.DrawString(vm.transaction.merchant.name.ToUpper(), font12B, Brushes.Black, headRect, stringFormatCenter);
                    yHeader += 15;

                    // if(vm.UserName!=null && vm.transaction.payment!="CASHIER"){
                        e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                        yHeader += 15;
                        e1.Graphics.DrawString("Customer:"+vm.transaction.user.fullname, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        yHeader += 15;
                        e1.Graphics.DrawString("Telp:"+vm.transaction.user.phone, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        yHeader += 15;
                    // }

                #endregion

                #region divider
                    e1.Graphics.DrawString("===================================================================================================================================", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                    yHeader += 10;
                #endregion

                #region container
                    e1.Graphics.DrawString("Table "+vm.transaction.tableNo, font22B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 25), stringFormatCenter);
                    yHeader += 35;
                    var now = DateTime.Now;
                    string day = now.Day<10?"0"+now.Day:now.Day.ToString();
                    string month = now.Month<10?"0"+now.Month:now.Month.ToString();
                    string year = now.Year<10?"0"+now.Year:now.Year.ToString();
                    string hour = now.Hour<10?"0"+now.Hour:now.Hour.ToString();
                    string minute = now.Minute<10?"0"+now.Minute:now.Minute.ToString();
                    string second = now.Second<10?"0"+now.Second:now.Second.ToString();
                    e1.Graphics.DrawString(day +" - " + month+" - " + year, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    e1.Graphics.DrawString(hour + ":" + minute+ ":" + second, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                    yHeader += 25;
                    foreach(var i in vm.transaction.listTransactionDetail){
                        e1.Graphics.DrawString(i.qty+" x "+i.listing.name.ToUpper(), font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        yHeader += 18;
                        if(i.remark.Length>0){
                            var numberLineBreaks = Regex.Matches(i.remark, @"\n").Count;
                            string[] remarks = i.remark.Split("\n");
                             e1.Graphics.DrawString("Remark : ", font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                                yHeader += 15;
                            for(int k=0; k<remarks.Length; k++){
                                e1.Graphics.DrawString(" * "+remarks[k], font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                                 yHeader += 18;
                            } 
                            if(numberLineBreaks<1){
                                 yHeader += 15;
                            }
                        }
                        e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                        yHeader += 15;
                    }
                    yHeader += 10;
                #endregion


            };

            try
            {
                p.Print();

            }
                catch (Exception ex)
            {
           }
        }
        public static void PrintReceipt(VMPrinter vm)
        {
            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = vm.PrinterName;
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                int yHeader = 0;
                int maxRight = 250;
                Rectangle headRect = new Rectangle(0, yHeader, maxRight, 500);
                #region header
                    e1.Graphics.DrawString(vm.transaction.merchant.name.ToUpper(), font12B, Brushes.Black, headRect, stringFormatCenter);
                    yHeader += 20;
                    e1.Graphics.DrawString("===================================================================================================================================", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                    yHeader += 10;
                    e1.Graphics.DrawString((vm.transaction.payment=="CASHIER"?"Cashier: ":"Customer:")+vm.transaction.user.fullname, font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    yHeader += 20;
                    e1.Graphics.DrawString("Telp:"+vm.transaction.user.phone, font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    yHeader += 20;
                #endregion

                #region divider
                    e1.Graphics.DrawString("===================================================================================================================================", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                    yHeader += 10;
                #endregion

                #region container
                    // e1.Graphics.DrawString("Cashier "+vm.transaction.merchant, font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    // yHeader += 15; 
                    e1.Graphics.DrawString("NO. TABLE "+vm.transaction.tableNo, font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    yHeader += 15;  
                    var now = DateTime.Now;
                    string day = now.Day<10?"0"+now.Day:now.Day.ToString();
                    string month = now.Month<10?"0"+now.Month:now.Month.ToString();
                    string year = now.Year<10?"0"+now.Year:now.Year.ToString();
                    string hour = now.Hour<10?"0"+now.Hour:now.Hour.ToString();
                    string minute = now.Minute<10?"0"+now.Minute:now.Minute.ToString();
                    string second = now.Second<10?"0"+now.Second:now.Second.ToString();

                    e1.Graphics.DrawString(day + " - " + month+" - " + year, font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    e1.Graphics.DrawString(hour + ":" + minute + ":" + second, font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                    yHeader += 25;
                    foreach(var i in vm.transaction.listTransactionDetail){
                        e1.Graphics.DrawString(i.listing.name.ToUpper(), font14B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        yHeader += 18; 
                        e1.Graphics.DrawString(i.qty+" x "+"@"+i.listing.price.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        e1.Graphics.DrawString((i.qty * i.listing.price).ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                        yHeader += 18;
                    }
                    e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                    yHeader += 18;
                    e1.Graphics.DrawString("SUBTOTAL", font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    e1.Graphics.DrawString(vm.transaction.totalBT.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                    yHeader += 18;
                    // if(vm.transaction.merchant.discount>0){
                        e1.Graphics.DrawString("DISCOUNT", font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        // var discount = vm.transaction.payment=="CASHIER"?"0":(vm.transaction.merchant.discount * vm.transaction.listTransactionDetail.Sum(x=>x.subtotal)/100).ToString("N0");
                        e1.Graphics.DrawString(vm.transaction.discount.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                        yHeader += 18;
                    // }
                    // if(vm.transaction.merchant.tax>0){
                        e1.Graphics.DrawString("TAX", font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        e1.Graphics.DrawString(vm.transaction.taxFee.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                        yHeader += 18;
                    // }
                    // if(vm.transaction.merchant.serviceTax>0){
                        e1.Graphics.DrawString("SERVICE FEE", font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                        e1.Graphics.DrawString(vm.transaction.serviceFee.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                        yHeader += 18;
                    // }
                    e1.Graphics.DrawString("TOTAL", font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatLeft);
                    e1.Graphics.DrawString(vm.transaction.total.ToString("N0"), font12B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatRight);
                    yHeader += 15;
                #endregion
                #region footer
                e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                yHeader += 15;
                e1.Graphics.DrawString("TERIMA KASIH ATAS KUNJUNGAN ANDA", font11B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatCenter);
                yHeader += 15;
                e1.Graphics.DrawString("SAMPAI JUMPA KEMBALI", font11B, Brushes.Black, new Rectangle(0, yHeader, maxRight, 15), stringFormatCenter);
                yHeader += 15;
                // e1.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------------------", font10, Brushes.Black, new Rectangle(0, yHeader, maxRight + 6, 15), stringFormatCenter);
                // yHeader += 15;
                #endregion
            };

            try
            {
                p.Print();

            }
                catch (Exception ex)
            {
           }
        }
    }
}