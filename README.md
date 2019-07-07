# AspNet

## Model
- Data
```
namespace MVCApplication.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "Example")]
        public bool IsTrue { get; set; }
    }
}



```

## View
- Visual
```
Home/About -> http://.....home/about



```

## Controller
- Coordinator
```
public ActionResult NameOfPage()
{
  View.Bag.Message="NameOfPage Page";
  return view();
}



```
