using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsService
{
    public class NewsService:INewsService
    {

        public bool uploadImage(byte[] imagedata, string imagename)
        {
            bool isSuccess = false;
            FileStream fileStream = null;
            //Get the file upload path store in web services web.config file.  
            string strTempFolderPath = @"C:\Users\ckrokad\Desktop\SOC project\NEWS\NewsService\NewsService\Images\";  // System.Configuration.ConfigurationManager.AppSettings.Get("FileUploadPath");
            try
            {

                if (!string.IsNullOrEmpty(strTempFolderPath))
                {
                    if (!string.IsNullOrEmpty(imagename))
                    {
                        string strFileFullPath = strTempFolderPath + imagename;
                        fileStream = new FileStream(strFileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        // write file stream into the specified file  
                        using (System.IO.FileStream fs = fileStream)
                        {
                            fs.Write(imagedata, 0, imagedata.Length);
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }

            return isSuccess;
        }

        public byte[] getImage(string imagename)
        {
            byte[] filedetails = new byte[0];
            //string strTempFolderPath = System.Configuration.ConfigurationManager.AppSettings.Get("FileUploadPath");
            string strTempFolderPath = @"C:\Users\ckrokad\Desktop\SOC project\NEWS\NewsService\NewsService\Images\";
            if (File.Exists(strTempFolderPath + imagename))
            {
                return File.ReadAllBytes(strTempFolderPath + imagename);
            }
            else return filedetails;
        }

        //==============================================================================

        public List<News> getAllNews(
            int? authorId = null,
            string tag = null,
            string newsCity = null)
        {

            using (var ctx = new Models.Model())
            {
                var newslist = ctx.news.AsQueryable();
                List<News> result = new List<News>();
                try
                {
                    if (authorId != null)
                    {
                        newslist = newslist.Where(x => x.author.authorId == authorId);
                    }
                    if (tag != null)
                    {
                        newslist = newslist.Where(x => x.tag == tag);
                    }
                    if (newsCity != null)
                    {
                        newslist = newslist.Where(x => x.newsCity == newsCity);
                    }
                    foreach (var n in newslist)
                    {
                        News n1 = new News();
                        n1.newsId = n.newsId;
                        n1.title = n.title;
                        n1.description = n.description;
                        n1.datetime = n.datetime;
                        n1.tag = n.tag;
                        n1.newsCity = n.newsCity;
                        n1.image = n.image;
                        n1.imagedata = getImage(n.image);

                        n1.author = new Author();
                        n1.author.authorId = n.author.authorId;
                        n1.author.authorName = n.author.authorName;
                        n1.author.authorImage = n.author.authorImage;
                        n1.author.authorCity = n.author.authorCity;

                        n1.author.imagedata = getImage(n.author.authorImage);
                        result.Add(n1);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                return result;

            }
            
        }

        public int addNews(News n)
        {
            using (var ctx = new Models.Model())
            {
                Models.News n1 = new Models.News();
                n1.title = n.title;
                n1.description = n.description;
                n1.datetime = DateTime.Now;
                n1.tag = n.tag;
                n1.newsCity = n.newsCity;
                n1.image = n.image;

                bool flag = uploadImage(n.imagedata,n.image);

                Models.Author a = ctx.authors.FirstOrDefault(x => x.authorId == n.author.authorId);
                n1.author = a;
                ctx.news.Add(n1);
                ctx.SaveChanges();
                return n1.newsId;
            }
        }

        public News getNews(int id)
        {
            using (var ctx = new Models.Model())
            {
                Models.News n = ctx.news.FirstOrDefault(x => x.newsId == id);
               
                News n1 = new News();
                
                n1.newsId = n.newsId;
                n1.title = n.title;
                n1.description = n.description;
                n1.datetime = n.datetime;
                n1.tag = n.tag;
                n1.newsCity = n.newsCity;
                n1.image = n.image;
                n1.imagedata = getImage(n.image);

                n1.author = new Author();
                n1.author.authorId = n.author.authorId;
                n1.author.authorName = n.author.authorName;
                n1.author.authorImage = n.author.authorImage;
                n1.author.authorCity = n.author.authorCity;
                n1.author.imagedata = getImage(n.author.authorImage);
                return n1;
            }
        }

        public void removeNews(int id)
        {
            using (var ctx = new Models.Model())
            {
                ctx.news.Remove(ctx.news.FirstOrDefault(x => x.newsId == id));
                ctx.SaveChanges();
            }
        }

        public News updateNews(News n)
        {
            using (var ctx = new Models.Model())
            {
                Models.News n1 = ctx.news.FirstOrDefault(x => x.newsId == n.newsId);
                if (n.title != null)
                {
                    n1.title = n.title;
                }

                if (n.description != null)
                {
                    n1.description = n.description;
                }

                
                n1.datetime = DateTime.Now;

                if (n.tag != null)
                {
                    n1.tag = n.tag;
                }

                if (n.newsCity != null)
                {
                    n1.newsCity = n.newsCity;
                }

                if (n.image != null)
                {
                    n1.image = n.image;
                }
                bool flag = uploadImage(n.imagedata, n.image);

                ctx.SaveChanges();
                return n;
            }
        }


        /*======================================================================*/

        public Author Login(string authorname, string password)
        {
            using (var ctx = new Models.Model())
            {
                Author a1 = null;
                try
                {
                    Models.Author a = ctx.authors.FirstOrDefault(x => x.authorName == authorname && x.password == password);
                    a1 = new Author(); 
                    a1.authorId = a.authorId;
                    a1.authorName = a.authorName;
                    a1.authorImage = a.authorImage;
                    a1.authorCity = a.authorCity;
                    a1.imagedata = getImage(a.authorImage);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                return a1;
            }
        }

        public int addAuthor(Author a)
        {
            using (var ctx = new Models.Model())
            {
                Models.Author a1 = new Models.Author();
                a1.authorName = a.authorName;

                a1.password = a.password;
                a1.authorImage = a.authorImage;
                a1.authorCity = a.authorCity;
                bool flag = uploadImage(a.imagedata, a.authorImage);
                ctx.authors.Add(a1);
                ctx.SaveChanges();
                //System.Diagnostics.Debug.WriteLine("Author ID &&&&"+a1.authorId);
                return a1.authorId;
            }
        }

        public List<Author> getAllAuthor()
        {
            using (var ctx = new Models.Model())
            {
                var authorlist = ctx.authors.AsParallel();
                List<Author> result = new List<Author>();
                foreach (var a in authorlist)
                {
                    Author a1 = new Author();
                   
                    a1.authorId = a.authorId;
                    a1.authorName = a.authorName;
                    a1.authorImage = a.authorImage;
                    a1.authorCity = a.authorCity;
                    a1.imagedata = getImage(a.authorImage);
                    result.Add(a1);
                }
                return result;
            }
        }

        public Author getAuthor(int id)
        {
            using (var ctx = new Models.Model())
            {
                Models.Author a = ctx.authors.FirstOrDefault(x => x.authorId == id);
                Author a1 = new Author();
               
                a1.authorId = a.authorId;
                a1.authorName = a.authorName;
                a1.authorImage = a.authorImage;
                a1.imagedata = getImage(a.authorImage);
                a1.authorCity = a.authorCity;
                return a1;
            }
        }

        public void removeAuthor(int id)
        {
            using (var ctx = new Models.Model())
            {
                ctx.authors.Remove(ctx.authors.FirstOrDefault(x => x.authorId == id));
                ctx.SaveChanges();
            }
        }

        public Author updateAuthor(Author a)
        {
            using (var ctx = new Models.Model())
            {
                Models.Author a1 = ctx.authors.FirstOrDefault(x => x.authorId == a.authorId);
                a1.authorId = a.authorId;
                if (a.authorName != null)
                {
                    a1.authorName = a.authorName;
                }
                if (a.authorImage != null)
                {
                    a1.authorImage = a.authorImage;
                }
                if (a.authorCity != null)
                {
                    a1.authorCity = a.authorCity;
                }
                bool flag = uploadImage(a.imagedata, a.authorImage);
                ctx.SaveChanges();
                return a;
            }
        }
    }
}
