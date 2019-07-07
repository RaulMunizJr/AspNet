# AspNet

## Model
- Data
> Model objects are the parts of the application that implement the logic for the application's data domain. Often, model objects retrieve and store model state in a database. For example, a Product object might retrieve information from a database, operate on it, and then write updated information back to a Products table in a SQL Server database.
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
> Views are the components that display the application's user interface (UI). Typically, this UI is created from the model data. An example would be an edit view of a Products table that displays text boxes, drop-down lists, and check boxes based on the current state of a Product object.
```
Home/About -> http://.....home/about



```

## Controller
- Coordinator
> Controllers are the components that handle user interaction, work with the model, and ultimately select a view to render that displays UI. In an MVC application, the view only displays information; the controller handles and responds to user input and interaction. For example, the controller handles query-string values, and passes these values to the model, which in turn might use these values to query the database.
```
public ActionResult NameOfPage()
{
  View.Bag.Message="NameOfPage Page";
  return view();
}



```
