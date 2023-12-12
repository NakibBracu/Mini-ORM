using MiniORM;

Course course = new Course();
course.Title = "Asp.Net";
course.Fees = 30000;
course.Id = 1;
course.Teacher = new Instructor()
{
    Id = 1,
    Name = "Jalaluddin",
    Email = "jalauddin123@gmail.com",
    PresentAddress = new Address
    {
        Id = 1,
        Street = "3",
        City = "Dhaka",
        Country = "Bangladesh"
    },
    PermanentAddress = new Address
    {
        Id = 2,
        Street = "5",
        // City = null,
        City = "Sylhet",
        Country = "Bangladesh"
    },
    PhoneNumbers = new List<Phone> {
        new Phone {Number = "123456",CountryCode="091",Extension="+330",Id = 1},
        new Phone {Number = "45689",CountryCode="0918",Extension="+390",Id = 2},
        new Phone {Number = "123456",CountryCode="09156",Extension="+3560",Id = 3},
    },
    // etc.
};
// all other fields set here.
course.Topics = new List<Topic>() {
    new Topic{ Id = 1 , Title= "Intro",Description= "Get to Know about Course",
        Sessions = new List<Session>{
        new Session{DurationInHour = 1,LearningObjective= "Overall Idea",Id = 1 },
        } },
    new Topic{ Id =2, Title= "Delegate",Description= "Advantages of deligates",
        Sessions = new List<Session>{
        new Session{DurationInHour = 2,LearningObjective= "Overall Idea about delegates" ,Id = 2},
        } },
    new Topic{Id = 3 ,Title= "Events",Description= "Delegate Alternatives",
        Sessions = new List<Session>{
        new Session{DurationInHour = 1,LearningObjective= "Overall Idea about uses of Func and Action",Id = 3 },
        } },
};
course.Tests = new List<AdmissionTest> {
new AdmissionTest{Id = 1,StartDateTime = new DateTime(2023,1,28,11,5,55),
    EndDateTime=  new DateTime(2023,1,28,12,5,55),
    TestFees = 100.00
},
new AdmissionTest{Id = 2, StartDateTime = new DateTime(2023,1,29,11,5,55),
    EndDateTime=  new DateTime(2023,1,29,12,5,55),
    TestFees = 100.00
}
};

var orm = new MyORM<int, Course>();
orm.Insert(course);


