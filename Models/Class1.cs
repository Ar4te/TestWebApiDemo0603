namespace Models;

public class Test
{
    public int Id { get; set; }
    public string Age { get; set; }
    public string Name { get; set; }
}

public class TestDto
{
    public TestDto(string age, string name)
    {
        Id = ++XXX._id;
        Age = age;
        Name = name;
    }
    public int Id { get; set; }
    public string Age { get; set; }
    public string Name { get; set; }
}

public class XXX
{
    public static int _id = 0;
}