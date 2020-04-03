# Specifikacija - Groblje

Ova specifikacija sadrži određene elemente za kreiranje baze podataka
groblja. Navedeni su TE, TP, kardinaliteti i obeležja pomoću kojih je opisana struktura groblja (kapela, krematorijum, grobna mesta - Urna, Sanduk), zaposlenih (šef groblja, tehničko osoblje) i odnosa člana porodice sa grobljem (prave ugovor sa šefom, primaju saučešće u kapeli)

Tekstualni opis ER konceptualne šeme baze podataka:

1. Groblje ima najmanje jedan krematorijum. Krematorijum pripada tačno jednom groblju.
1. Groblje ima najmanje jednu kapelu. Kapela pripada tačno jednom groblju.
1. Groblje sadrži nijedno ili više grobnih mesta. Jedno grobno mesto pripada tačno jednom groblju.
1. Grobno mesto može biti kovčeg ili urna.
1. U kapeli jedan ili više članova porodice primaju saučešće. Članovi porodice primaju saučešće u tačno jednoj kapeli.
1. Čovek je član jedne ili više porodica. Porodica je sadržana od jednog ili više čoveka.
1. IMKU (Izvod iz matične knjige umrlih) se odnosi na tačno jednog čoveka. Jedan čovek može da ima nijedan (ako je živ) ili jedan IMKU.
1. Radnik je tačno jedan čovek. Jedan čovek može, a ne mora da bude radnik.
1. Radnik može da bude Tehničko osoblje, ili Šef.
1. Groblje ima tačno jednog šefa. Jedan šef groblja može da bude šef jednog ili više groblja.
1. Šef groblja nadzire jednog ili više radnika tehničkog osoblja. Radnik tehničkog osoblja ima tačno jednog šefa.
1. Član porodice pravi ugovor sa tačno jednim šefom. Jedan šef groblja pravi ugovor sa jednim ili više članova porodice.
1. U ugovoru član porodice bira vrstu sahrane koja može biti: ukopavanje ili kremiranje (`dom(VRSTA_SAHRANE)={ukopavanje, kremiranje}`)
1. Kapela/Groblje/Krematorijum/Grobno mesto sadrže tačno jednu lokaciju. Jedna lokacija može sadržati jednu ili nijednu Kapelu/Groblje/Krematorijum/Grobno mesto.