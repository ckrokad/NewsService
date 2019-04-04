using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsService
{
    public class NewsService:INewsService
    {
        public List<News> getAllNews(
            int? authorId = null,
            string tag = null,
            string newsCity = null)
        {

            using (var ctx = new Models.Model())
            {
                var newslist = ctx.news.AsParallel();
                List<News> result = new List<News>();

                if (authorId != null)
                {
                    newslist = newslist.Where(x => x.author.authorId == authorId);
                }
                if(tag != null)
                {
                    newslist = newslist.Where(x => x.tag == tag);
                }
                if(newsCity != null)
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

                    n1.author = new Author();
                    n1.author.authorId = n.author.authorId;
                    n1.author.authorName = n.author.authorName;
                    n1.author.authorImage = n.author.authorImage;
                    n1.author.authorCity = n.author.authorCity;
                    result.Add(n1);
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
                n1.datetime = n.datetime;
                n1.tag = n.tag;
                n1.newsCity = n.newsCity;
                n1.image = n.image;

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

                n1.author = new Author();
                n1.author.authorId = n.author.authorId;
                n1.author.authorName = n.author.authorName;
                n1.author.authorImage = n.author.authorImage;
                n1.author.authorCity = n.author.authorCity;
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
                n1.title = n.title;
                n1.description = n.description;
                n1.datetime = n.datetime;
                n1.tag = n.tag;
                n1.newsCity = n.newsCity;
                n1.image = n.image;
                ctx.SaveChanges();
                return n;
            }
        }


        /*======================================================================*/
        public int addAuthor(Author a)
        {
            using (var ctx = new Models.Model())
            {
                Models.Author a1 = new Models.Author();
                a1.authorName = a.authorName;
                a1.authorImage = a.authorImage;
                a1.authorCity = a.authorCity;
                ctx.authors.Add(a1);
                ctx.SaveChanges();
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
                a1.authorName = a.authorName;
                a1.authorImage = a.authorImage;
                a1.authorCity = a.authorCity;
                ctx.SaveChanges();
                return a;
            }
        }
    }
}
