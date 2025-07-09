För att seeda databasen med testdata finns en SeedData-metod som körs vid uppstart om "SeedData" i
"appsettings.Development.json" är satt till true. Då töms tabellerna och ny data fylls på.

När seedningen är klar, sätt "SeedData" till false.
