# Specifikacija - Groblje

Ova specifikacija sadrži određene elemente za kreiranje baze podataka
groblja. Navedeni su TE, TP, kardinaliteti i obeležja pomoću kojih je opisana struktura groblja (kapela, krematorijum, grobna mesta - Urna, Sanduk), zaposlenih (šef groblja, tehničko osoblje) i odnosa člana porodice sa grobljem (prave ugovor sa šefom, primaju saučešće u kapeli)

## Tekstualni opis ER konceptualne šeme baze podataka

1. Groblje ima najmanje jedan krematorijum. Krematorijum pripada tačno jednom groblju.
1. Groblje ima najmanje jednu kapelu. Kapela pripada tačno jednom groblju.
1. Groblje sadrži nijedno ili više grobnih mesta. Jedno grobno mesto pripada tačno jednom groblju.
1. Grobno mesto može biti kovčeg ili urna.
1. Čovek je član jedne ili više porodica. Porodica je sadržana od jednog ili više čoveka.
1. IMKU (Izvod iz matične knjige umrlih) se odnosi na tačno jednog čoveka. Jedan čovek može da ima nijedan (ako je živ) ili jedan IMKU.
1. IMKU se odnosi na nijedno ili jedno grobno mesto. Jedno grobno mesto se odnosi na tačno jedan IMKU.
1. Radnik je tačno jedan čovek. Jedan čovek može, a ne mora da bude radnik.
1. Radnik može da bude Tehničko osoblje, ili Šef.
1. Groblje ima tačno jednog šefa. Jedan šef groblja može da bude šef jednog ili više groblja.
1. Šef groblja nadzire jednog ili više radnika tehničkog osoblja. Radnik tehničkog osoblja ima tačno jednog šefa.
1. Član porodice pravi ugovor sa tačno jednim šefom. Jedan šef groblja pravi ugovor sa jednim ili više članova porodice.
1. U ugovoru član porodice bira tip sahrane koji može biti: ukopavanje ili kremiranje (`dom(TIP_SAHRANE)={ukopavanje, kremiranje}`)
1. Član porodice koji je sklopio ugovor sa šefovom zauzima tačno jednu kapelu za primanje saučešća. Jedna kapela može biti zauzeta od nijednog ili jednog člana porodice određen vremenski period.
1. Kapela/Groblje/Krematorijum/Grobno mesto se nalaze na tačno jednoj lokaciji. Na jednoj lokaciji se može nalaziti nijedna ili jedna Kapela/Groblje/Krematorijum/Grobno mesto.

## Tabelarni prikaz TE/TP i njihovih obeležja

| TIP ENTITETA | TIP POVEZNIKA | OBELEŽJA |
| --- | --- | --- |
| Groblje | ima, sadrži, se nalazi | groblje_id, naziv |
| Grobno mesto | is-a, se odnosi, se nalazi | grobno_mesto_id |
| Urna | is-a | kapacitet |
| Kovceg | is-a | marka_kovcega |
| Krematorijum | pripada, se-nalazi | krematorijum_id, naziv |
| IMKU |  se_odnosi | covek_id, vreme_umiranja, slika |
| Lokacija | se nalazi | lokacija_id, x, y |
| Kapela | pripada, se nalazi, primaju sucesce | kapela_id, naziv |
| Covek |  je, sadržan, se odnosi | covek_id, ime, prezime, datum_rodjenja |
| Clan Porodice | je, sadrzi | id_coveka (npr. preminulog), id_clana_porodice (npr. sestra od preminulog) |   
| Radnik | is-a, je | covek_id, radno_vreme (od-do) |
| Tehnicko osoblje | is-a | |
| Sef | is-a, nadzire | |
| Ugovor | prave ugovor, (zauzima kapelu - Gerund) | id_clana_porodice, id_sefa, tip_sahrane, vreme_sahrane |
