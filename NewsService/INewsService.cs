using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NewsService
{
    [ServiceContract]
    public interface INewsService
    {
        [OperationContract]
        bool uploadImage(byte[] imagedata, string imagename);

        [OperationContract]
        byte[] getImage(string imagename);

        [OperationContract]
        List<News> getAllNews(
            int? authorId = null, 
            string tag = null,
            string newsCity = null);

        [OperationContract]
        News getNews(int id);

        [OperationContract]
        int addNews(News news, byte[] imagedata);

        [OperationContract]
        void removeNews(int id);

        [OperationContract]
        News updateNews(News news, byte[] imagedata);

        /*===============================*/
        [OperationContract]
        List<Author> getAllAuthor();

        [OperationContract]
        Author Login(string authorname, string password);

        [OperationContract]
        Author getAuthor(int id);
        
        [OperationContract]
        int addAuthor(Author author, byte[] imagedata);

        [OperationContract]
        void removeAuthor(int id);

        [OperationContract]
        Author updateAuthor(Author author, byte[] imagedata);

    }
    [DataContract]
    public class News
    {
        [DataMember]
        public int newsId { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public byte[] imagedata { get; set; }
        [DataMember]
        public string tag { get; set; }
        [DataMember]
        public string newsCity { get; set; }
        [DataMember]
        public DateTime datetime { get; set; }
        [DataMember]
        public Author author { get; set; }
    }

    [DataContract]
    public class Author
    {
        [DataMember]
        public int authorId { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string authorName { get; set; }
        [DataMember]
        public string authorImage { get; set; }
        [DataMember]
        public byte[] imagedata { get; set; }
        [DataMember]
        public string authorCity { get; set; }
       
    }
}
