/// Build XML session from Command string in quotes returning XML response from Computrition Web Services Gateway
/// Expect XML command line input

using System;
using System.Text;
using System.Net;
using System.IO;


namespace ComputritionMenuGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Syntax: ComputritionMenuGateway Command");
            } else
            {
                string ComputritionGatewayCommand = args[0]; //pass command line string
               try
                {
                    string cgiPath = "http://computritionserver:8090/hsws/" + ComputritionGatewayCommand;
                    var client = new WebClient();
                    string html = client.DownloadString(cgiPath);

/// Manipulate nutrient XML data
/// 
///input:  nutrients="#1#|#2#|#3#|#4#|#5#|#6#|#7#|#8#|#9#|#10#|#11#|#12#|#13#|#14#|#15#|#16#|#17#|#18#|#19#|#20#|#21#|#22#|#23#|#24#|#25#|#26#|"
///focus:  pattern = "@"nutrients=\"/[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|[0-9]|\\"$"
///output: nutrients_calories="#1#" nutrients_fat="#3#" nutrients_sodium="#9#" nutrients_carbs="#11#" nutrients_protein="#14#"

                    int startholder = html.IndexOf("nutrients=\"", 1) - 1;                   /// Start at first instance of nutrients

                    if (startholder < 1)                                                     /// If no nutrients in XML
                    {
                        Console.WriteLine(html);                                            /// leave XML unmolested
                        System.Environment.Exit(0);                                         /// leave utility
                    }

                    int endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 1st value (Length)
                    string passon = html.Substring(0, startholder);                          /// include prefix string

                    while (startholder > 0)
                    {
                        // Calories
                        startholder = html.IndexOf("nutrients=\"", startholder) - 1;         /// Start at next instance of nutrients 
                        startholder += 12;                                                   /// skip nutrient="
                        endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 1st value (Length)
                        passon += " nutrients_calories=\"";                                  /// label new field
                        if (endholder > 0)                                                   /// insert 1st value
                        {
                            passon += html.Substring(startholder, endholder);                /// if there is anything to insert
                        }
                        passon += "\" ";                                                     /// close new first value

                        // Fats
                       startholder = html.IndexOf("|", startholder) + 1;                    /// skip 2nd value
                       startholder = html.IndexOf("|", startholder) + 1;                    /// moved to 3rd value
                        endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 3rd value
                        passon += "nutrients_fat=\"";                                        /// label new field
                        if (endholder > 0)                                                   /// insert 3rd value
                        {
                            passon += html.Substring(startholder, endholder);                /// if there is anything to insert
                        }
                        passon += "\" ";                                                     /// close new second value

                        // sodium
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 4th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 5th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 6th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 7th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 8th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// moved to 9th value
                        endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 9th value
                        passon += "nutrients_sodium=\"";                                     /// label new field
                        if (endholder > 0)                                                   /// insert 9th value
                        {
                            passon += html.Substring(startholder, endholder);                /// if there is anything to insert
                        }
                        passon += "\" ";                                                     /// close new third value

                        // carbs
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 10th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// moved to 11th value
                        endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 11th value
                        passon += "nutrients_carbs=\"";                                      /// label new field
                        if (endholder > 0)                                                   /// insert 11th value
                        {
                            passon += html.Substring(startholder, endholder);                /// if there is anything to insert
                        }
                        passon += "\" ";                                                     /// close new fourth value

                        // proteins
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 12th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// skip 13th value
                        startholder = html.IndexOf("|", startholder) + 1;                    /// moved to 14th value
                        endholder = html.IndexOf("|", startholder) - startholder;            /// get end of 14th value
                        passon += "nutrients_proteins=\"";                                   /// label new field
                        if (endholder > 0)                                                   /// insert 14th value
                        {
                            passon += html.Substring(startholder, endholder);                /// if there is anything to insert
                        }
                        passon += "\" ";                                                     /// close new fifth value

                        /// Check for next
                        startholder = html.IndexOf("\"", startholder) + 1;                   /// skip over rest of nutrients data 
                        endholder = html.IndexOf("nutrients=\"", startholder) - startholder; /// Start at next instance of nutrients 
                        if (endholder > 0)
                        {
                            passon += html.Substring(startholder, endholder - 1);            /// get to next recipe
                        }
                        else
                        {
                            passon += html.Substring(startholder);                           /// get rest of XML
                            startholder = 0;
                     }  }


                    /// Debug
                    // Console.WriteLine("startholder: {0}",startholder);
                    // Console.WriteLine("endholder: {0}", endholder);

/// End manupliation, time to send it out
                Console.WriteLine(passon);

                }
                catch (Exception exception)
                {
                    //if an error occurs with in the try block, it will handled here.
                }
            }
        }
    }
}
