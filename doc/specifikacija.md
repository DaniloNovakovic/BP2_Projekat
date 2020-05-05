# Specifikacija - Groblje

Ova specifikacija sadrži određene elemente za kreiranje baze podataka
groblja. Navedeni su TE, TP, kardinaliteti i obeležja pomoću kojih je opisana struktura jednog pogrebnog preduzeća, kao i način na koji član porodice preminulog sklapa ugovor sa šefom groblja i zauzima kapelu.

## Tekstualni opis ER konceptualne šeme baze podataka

1. IMKU (Izvod iz matične knjige umrlih) se odnosi na tačno jednog čoveka. Jedan čovek može da ima nijedan (ako je živ) ili jedan IMKU. IMKU sadrži informacije o vremenu umiranja, kao i id čoveka preko kojeg se jedinstveno identifikuje.
1. IMKU se odnosi na nijedno ili jedno grobno mesto. Jedno grobno mesto se odnosi na tačno jedan IMKU. Grobno mesto sadrži svoj jedinstven identifikacioni broj 'grobno_mesto_id'.
1. Grobno mesto može biti kovčeg ili urna. Kovčeg sadrži informaciju o marki kovčega, dok urna o kapacitetu/količini pepela.
1. Čovek može, a nemora biti član porodice preminulog. Jedan preminuo čovek može imati nijednog ili više članova porodice. Član porodice je identifikovan pomoću ključa preminulog i živog čoveka (K={id_covek, id_clan_porodice}), takođe poseduje tip veze (npr. "sestra")
1. Radnik je tačno jedan čovek. Jedan čovek može, a ne mora da bude radnik. Radnik sadrži informaciju o radnom vremenu kao i id čoveka pomoću kojeg se jedinstveno identifikuje.
1. Radnik može da bude Tehničko osoblje, ili Šef.
1. Šef groblja nadzire nijednog ili više radnika tehničkog osoblja. Radnik tehničkog osoblja ima nijednog ili jednog šefa.
1. Član porodice preminulog može da pravi ugovor sa nijednim ili jednim šefom groblja. Jedan šef groblja pravi ugovor sa nijednim ili više članova porodice.
1. U ugovoru član porodice bira tip sahrane koji može biti: ukopavanje ili kremiranje (`dom(TIP_SAHRANE)={ukopavanje, kremiranje}`)
1. Član porodice preminulog koji je sklopio ugovor sa šefovom zauzima tačno jednu kapelu za primanje saučešća. Jedna kapela može biti zauzeta od nijednog ili jednog člana porodice određen vremenski period.
1. Kapela/Grobno mesto se nalaze na tačno jednoj lokaciji. Na jednoj lokaciji se može nalaziti nijedna ili jedna Kapela/Grobno mesto.

## Tabelarni prikaz TE/TP i njihovih obeležja

| TIP ENTITETA | TIP POVEZNIKA | OBELEŽJA |
| --- | --- | --- |
| Grobno mesto | is-a, počiva, se nalazi | grobno_mesto_id |
| Urna | is-a | kapacitet |
| Kovceg | is-a | marka_kovcega |
| IMKU |  počiva, se odnosi | covek_id, vreme_umiranja |
| Lokacija | se nalazi | lokacija_id, x, y |
| Kapela | sadrži, se nalazi, zauzima | kapela_id, naziv |
| Covek |  je, se odnosi | covek_id, ime, prezime, datum_rodjenja |
| Clan Porodice | je, pravi ugovor | id_coveka (npr. preminulog), id_clana_porodice (npr. sestra od preminulog), tip_veze (npr. "sestra") |
| Radnik | is-a, je | covek_id, radno_vreme (od-do) |
| Tehnicko osoblje | is-a, nadzire | |
| Sef | is-a, nadzire, ima | |
| Ugovor | pravi ugovor, (zauzima kapelu - Gerund) | id_clana_porodice, id_sefa, id_kapele, tip_sahrane, vreme_sahrane |
