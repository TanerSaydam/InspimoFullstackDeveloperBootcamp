# Middlewares
Uygulamada her request'de yaz�ld��� s�rayla �al���p yap�lan iste�i yani context nesnemizi alarak gerekli kontrol ya da i�lemi yapt�ktan sonra s�radaki middleware'e aktaran yap�lard�r.
Kullanmak i�in IMiddleware interface'ini bir class'a implement edebiliriz, ya da app.Use(async (context,next) => {await next(context)}) ile kullanabiliriz.

E�er IMiddleware y�ntemini kullan�rsak mutlaka DI yapmam�z ve middleware'i app.useMiddleware ile �a��rmam�z gerekir.

Genel kontrol i�in kullan�labilir
�lk user� olu�turma
Database migration otomatikle�tirme
Exception handler => hata i�leme
vb i�lemler i�in kullan�labilir



# Filters
Filter �e�idine g�re metodun belirli bir par�as�nda araya girip istenilen bir i�lemi ger�ekle�tirir. Kullanmak i�in bireysel olarak metot bazl� �a�r�lmal�d�r. Genelde Attribute'e d�n��t�r�l�p kullan�l�r.
�rne�in : 
Metot �al��madan �nce log kayd� yapmak istiyorsak kullanabilir
Metot sonunda Cacheleme yapmak istiyorsak kulan�labilir
Metot �al��madan �nce Authentication, Authorization i�lemleri yapmak istiyorsak kullanabilir

## Filter Listesi
### IActionFilter
�ki tane metoda sahip, Executing ve Executed.
Bu metotlar eklendi�i metodun ba��nda veya sonunda �al���r, Context nesnemizi Body okuyabilecek �ekilde bize verir ve normalde Body k�sm� kitli gelirken Filter sayesinde a��k �ekilde elde edebiliriz.
Genelde Cachele, Loglama i�lemleri i�in kullan�l�r


