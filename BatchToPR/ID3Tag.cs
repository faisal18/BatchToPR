using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TagLib;


namespace BatchToPR
{




    public class ID3Tag
    {

        public void ID3Control()
        {

            string songsdir = @"C:\tmp\ID3Project\Songs\";
            string imagedir = @"C:\tmp\ID3Project\Images\";
            string filename = string.Empty;
            string ImageURL = string.Empty;
            string extension = "flac";
           
            string searchterm = "Official";
            string[] SongFiles = null;
            int count = 0;
            int error = 0;
            int index = 0;

            try
            {
                SongFiles = Directory.GetFiles(songsdir, "*.flac");
                if (SongFiles.Length == 0)
                {
                    SongFiles = Directory.GetFiles(songsdir, "*.mp3");
                    extension = "mp3";
                }
                foreach (string file in SongFiles)
                {
                    

                    filename = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine("Working on file " + filename);

                    ImageURL = GetImageFromYoutube(filename, searchterm,index);

                    //ImageURL = GetImageFromAmazon(filename,searchterm);
                    //ImageURL = GetImageFromGenius(filename, searchterm);

                    string imagename = imagedir + filename + ".jpg";
                    if (ImageURL != "fail")
                    {
                        if (SaveImageinFolder(ImageURL, imagename))
                        {
                            Console.WriteLine("Image Saved");
                            if (UpdateID3Tag(songsdir + filename + "." + extension, imagename) == "success")
                            {
                                Console.WriteLine("ID3 Tag Updated");
                                count++;
                            }
                            else
                            {
                                error++;
                                MoveFile(file);
                            }
                        }
                        else
                        {
                            error++;
                            MoveFile(file);
                        }
                    }
                    else
                    {
                        error++;
                        MoveFile(file);
                    }

                }
                Console.WriteLine("Total Files: " + SongFiles.Length);
                Console.WriteLine("Files updated: " + count);
                Console.WriteLine("Error occured: " + error);
                Console.Read();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




        }

        public void MoveFile(string file)
        {
            Console.WriteLine("Failed to update file ");
            System.IO.File.Delete(@"C:\tmp\ID3Project\Error\" + Path.GetFileName(file).ToString());
            System.IO.File.Move(Path.GetFullPath(file), @"C:\tmp\ID3Project\Error\" + Path.GetFileName(file).ToString());
        }

        public string GetImageFromYoutube(string filename,string query,int index)
        {
            //string url = "https://www.youtube.com/results?search_query=in+the+end+linkin+park+album";

            string queryfilename = filename.Replace(' ', '+');
            queryfilename = queryfilename.Replace("&", "%26");
            queryfilename = queryfilename.Replace("(", "%28");
            queryfilename = queryfilename.Replace(")", "%29");

            string url = "https://www.youtube.com/results?search_query=" + queryfilename + "+" + query;
            string imageurl = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url);

                    s = WebUtility.HtmlDecode(s);
                    HtmlDocument result = new HtmlDocument();
                    result.LoadHtml(s);
                    //List<HtmlNode> toftitle = result.DocumentNode.Descendants().Where
                    //    (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("yt-thumb video-thumb"))).ToList();


                    List<HtmlNode> toftitle = result.DocumentNode.Descendants().Where
                        (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("yt-thumb video-thumb"))).ToList();

                    imageurl = toftitle[index].LastChild.FirstChild.NextSibling.Attributes["src"].DeEntitizeValue.ToString();
                    if(!imageurl.Contains("https://"))
                    {
                        imageurl = @"https:" + imageurl;
                    }
                    return imageurl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "fail";
            }
        }

        public string GetImageFromGenius(string filename,string query)
        {
            string queryfilename = filename.Replace(" ", "%20");
            queryfilename = queryfilename.Replace("&", "%26");

            string url = "https://genius.com/search?q=" + queryfilename + query;
            string imageurl = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url);

                    s = WebUtility.HtmlDecode(s);
                    HtmlDocument result = new HtmlDocument();
                    result.LoadHtml(s);
                    //List<HtmlNode> toftitle = result.DocumentNode.Descendants().Where
                    //    (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("yt-thumb video-thumb"))).ToList();


                    List<HtmlNode> toftitle = result.DocumentNode.Descendants().Where
                        (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("yt-thumb video-thumb"))).ToList();

                    imageurl = toftitle[0].LastChild.FirstChild.NextSibling.Attributes["src"].DeEntitizeValue.ToString();
                    return imageurl;
                }
            }
            catch (Exception)
            {
                return "fail";
            }
        }

        public string GetImageFromAmazon(string query)
        {
            string url = "https://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Daps&field-keywords=" + query;
            string imageurl = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(url);

                    s = WebUtility.HtmlDecode(s);
                    HtmlDocument result = new HtmlDocument();
                    result.LoadHtml(s);

                    List<HtmlNode> toftitle = result.DocumentNode.Descendants().Where
                        (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("a-section a-spacing-none a-inline-block s-position-relative"))).ToList();

                    imageurl = toftitle[1].FirstChild.FirstChild.Attributes["srcset"].DeEntitizeValue.ToString();

                    string[] differences = { ","};
                    string[] imgurlarray = imageurl.Split(differences, StringSplitOptions.RemoveEmptyEntries);
                    int max = imgurlarray.Length;

                    imageurl = imgurlarray[imgurlarray.Length-1];
                    imageurl = imageurl.Remove(imageurl.Length - 3);


                    return imageurl;
                }
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        public bool SaveImageinFolder(string imageURL, string imageName)
        {
            bool result = false;
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(imageURL, imageName);
                result = true;
                return result;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public string UpdateID3Tag(string SongUrl, string PictureUrl)
        {
            try
            {

                using (var file = TagLib.File.Create(SongUrl))
                {
                    file.RemoveTags(TagTypes.AllTags);
                    file.Save();
                }

                using (var file = TagLib.File.Create(SongUrl))
                {

                    string[] differences = { "-" };
                    string[] imgurlarray = Path.GetFileNameWithoutExtension(SongUrl).Split(differences, StringSplitOptions.RemoveEmptyEntries);

                    IPicture newArt = new Picture(PictureUrl);
                    file.Tag.Pictures = new IPicture[1] { newArt };
                    if (imgurlarray.Length>1)
                    {
                        file.Tag.Title = imgurlarray[1];
                    }
                    else
                    {
                        file.Tag.Title = imgurlarray[0];
                    }
                    file.Tag.AlbumArtists = new[] { imgurlarray[0] };
                    file.Tag.Album = imgurlarray[0];
                    file.Save();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }

}
