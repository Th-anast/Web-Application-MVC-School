# Web Application MVC School
Η διαδικτυακή εφαρμογή MVC School επιτρέπει σύνδεση χρηστών με διαφορετικές λειτουργίες ανά κατηγορία. Οι φοιτητές μπορούν να δουν τη βαθμολογία τους ανά μάθημα, εξάμηνο και συνολικά για όλα τα εξετασμένα μαθήματα. Οι καθηγητές μπορούν να δουν βαθμολογίες για βαθμολογημένα μαθήματα και να καταχωρήσουν βαθμολογίες για μη βαθμολογημένα μαθήματα. Οι γραμματείες μπορούν να καταχωρήσουν μαθήματα, καθηγητές και φοιτητές, να δουν μαθήματα, να αναθέσουν μαθήματα σε καθηγητές και να δηλώσουν μαθήματα σε φοιτητές.

Η εφαρμογή MVC School χρησιμοποιεί βάση δεδομένων που δημιουργήθηκε με το SQL Server Management Studio (SSMS). Για την ενσωμάτωση της βάσης δεδομένων στο project, απαιτείται η εκτέλεση scaffolding με την ακόλουθη εντολή:

```ruby
Scaffold-DbContext -f "Server={System Name}\SQLEXPRESS;Database=school_db;Trusted_Connection=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context "SchoolDBContext" -DataAnnotations
```

Αυτή η εντολή δημιουργεί τα μοντέλα και το DbContext (SchoolDBContext) στο φάκελο Models, χρησιμοποιώντας το Microsoft.EntityFrameworkCore.SqlServer και DataAnnotations για τη διαχείριση της βάσης δεδομένων school_db. Αντικαταστήστε το `{System Name}` με το όνομα του συστήματός σας.
