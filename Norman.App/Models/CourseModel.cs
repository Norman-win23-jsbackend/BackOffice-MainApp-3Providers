namespace Norman.App;

public class CourseModel
{
    public string? Id { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public List<string>? Categories { get; set; }
    public string? ImgUrl { get; set; }
    public string? ImgHeaderUrl { get; set; }
    public string? Discriminator { get; set; }
    public string? Ingress { get; set; }
    public string? Reviews { get; set; }
    public double? StarRating { get; set; }
    public string? Title { get; set; }
    public string? Price { get; set; }
    public string? Discount { get; set; }
    public string? Hours { get; set; }
    public string? LikePercentage { get; set; }
    public string? Likes { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorImageUrl { get; set; }
    public string? AuthorDescription { get; set; }
    public string? CourseDescription { get; set; }
    public string? CourseGoal1 { get; set; }
    public string? CourseGoal2 { get; set; }
    public string? CourseGoal3 { get; set; }
    public string? CourseGoal4 { get; set; }
    public string? CourseGoal5 { get; set; }
    public string? CourseIncludes1 { get; set; }
    public string? CourseIncludes2 { get; set; }
    public string? CourseIncludes3 { get; set; }
    public string? CourseIncludes4 { get; set; }
    public string? CourseIncludes5 { get; set; }
    public string? ProgramDetailTitle1 { get; set; }
    public string? ProgramDetailDescription1 { get; set; }
    public string? ProgramDetailTitle2 { get; set; }
    public string? ProgramDetailDescription2 { get; set; }
    public string? ProgramDetailTitle3 { get; set; }
    public string? ProgramDetailDescription3 { get; set; }
    public string? ProgramDetailTitle4 { get; set; }
    public string? ProgramDetailDescription4 { get; set; }
    public string? ProgramDetailTitle5 { get; set; }
    public string? ProgramDetailDescription5 { get; set; }
    public string? ProgramDetailTitle6 { get; set; }
    public string? ProgramDetailDescription6 { get; set; }
    public int? Followers { get; set; }
    public int? Subscribers { get; set; }
}
