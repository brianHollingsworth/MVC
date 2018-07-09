using System;

// Models: Classes that represent the data of the app.
// The model classes use validation logic to enforce business rules for that data.
// Typically, model objects retrieve and store model state in a database.
// In this tutorial, a Movie model retrieves movie data from a database, provides it to the view or updates it.
// Updated data is written to a database.
namespace MvcMovie.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}