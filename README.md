dotnet ef (migrations commands):

!! WYKONUJEMY TO Z FOLDERU Infrastructure !!
!! dotnet ef -- musi wyswietlac jednorozca.

dotnet ef migrations add ${nazwa} -s ${relatywna_sciezka_do_projektu_api} -o Sql/Migrations 
dotnet ef database update -s ${relatywna_sciezka_do_projektu_api} 
