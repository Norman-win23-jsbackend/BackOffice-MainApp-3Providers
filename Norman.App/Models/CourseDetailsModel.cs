namespace Norman.App.Models;

public class CourseDetailsModel
{
    public string Id { get; set; }
    public List<string> Categories { get; set; }
    public string? Discriminator { get; set; }
    public string? Hours { get; set; }
    public string? ImgHeaderUrl { get; set; }
    public string? ImgUrl { get; set; }
    public string? Ingress { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public string? LikePercentage { get; set; }
    public string? Likes { get; set; }
    public string? Reviews { get; set; }
    public double StarRating { get; set; }
    public string? Title { get; set; }
    public string? id { get; set; }
    public Authors Authors { get; set; }
    public CategoryEntity CategoryEntity { get; set; }
    public Content Content { get; set; }
    public Prices Prices { get; set; }
}

public class Authors
{
    public int Id { get; set; }
    public string? AuthorDescription { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? AuthorName { get; set; }
    public int Followers { get; set; }
    public int Subscribers { get; set; }
}

public class CategoryEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class Content
{
    public string? Description { get; set; }
    public List<string> Includes { get; set; }
    public List<ProgramDetail> ProgramDetails { get; set; }
}

public class ProgramDetail
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
}

public class Prices
{
    public string? Currency { get; set; }
    public string? Discount { get; set; }
    public string? Price { get; set; }
}
