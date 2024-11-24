namespace Blogger.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; } //Unique Identifier
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageURL { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Navigation Property
        public ICollection<Tags> Tags { get; set; }

    }
}
