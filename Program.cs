namespace RoskildeDyreinternat
{

    class Program
    {
        static void Main(string[] args)
        {
            #region Alle Objekter 
            // Opretter en ny instans af KundeRepo
            var besøgRepo = new BesøgRepo();
            var brugerRepo = new BrugerRepo(besøgRepo);
            brugerRepo.OpretKunde(kunde1);
            brugerRepo.OpretKunde(kunde2);


            DyrRepo repo = new DyrRepo();

                repo.dyrDictionary.Add(1, new Hund(true, true, "Stella", "race", 1, "hun", 3, "sund", false));
                repo.dyrDictionary.Add(2, new Hund(true, false, "Bob", "ukendt", 3, "han", 2, "sund", false));
                repo.dyrDictionary.Add(3, new Hund(false, false, "Dennis", "ukendt", 2, "han", 6, "mangler et ben", false));

                repo.dyrDictionary.Add(99, new Kat(true, true, "Denas", "Siamese", 99, "han", 5, "Sund", false));
                repo.dyrDictionary.Add(98, new Kat(true, false, "Hansen", "Norsk Skovkat", 98, "hun", 2, "Mangler et øje", false));
                repo.dyrDictionary.Add(97, new Kat(false, true, "Emil", "Maine Coon", 97, "han", 12, "Har sukkersyge", false));

                foreach (var entry in repo.dyrDictionary)
                {
                    Console.WriteLine($"ID: {entry.Key}, Navn: {entry.Value.Navn}");
                    Console.WriteLine(entry.Value.LavLyd());
                }
            }

            //Dette er de kunder, der er oprettet
            Kunde kunde1 = new Kunde(99, "Ida", "IdaEmail", "1234567", "Højvej 1", "Kunde", 23, "Mand");
            Kunde kunde2 = new Kunde(98, "Lone", "LoneEmail", "7654321", "Blomstervej", "Kunde", 67, "Kvinde");
            Kunde kunde3 = new Kunde(97, "Læse", "Læse@mail.com", "12345678", "Durumvej 20", "Kunde", 34, "Mand");
            Kunde kunde4 = new Kunde(96, "Luke", "luke@gmsil.com", "2323232", "Sezzamvej35 2.tv", "Kunde", 36, "Mand");


            //Dette er de medarbejder, der er oprettet
            Medarbejder medarbejder1 = new Medarbejder(1, "Emil", "emil@hund.dk", "+45 232323", "Denvej 7", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder2 = new Medarbejder(2, "Emma", "emma@kat.dk", "+45 34 34 34 34", "Gladvej 2", "Medarbejder", "Dyrpasser", 37);
            Medarbejder medarbejder3 = new Medarbejder(3, "Erik", "erik@doctor.dk", "+45 11 11 22 22", "Flotvej 63", "Medarbejder", "Dyrlæge", 35);


           



            //Dette er de besog, der er oprettet
            Besøg besøg1 = new Besøg(new DateTime(2025, 4, 15), kunde1, );
            Besøg besøg2 = new Besøg(new DateTime(2025, 3, 17), kunde2, kat1);
            #endregion
            besøgRepo.AddBesøg(besøg1);
            besøgRepo.AddBesøg(besøg2);


            //////SLET MÅSKE
            //dyrRepo.TilføjDyr(hund1);
            //dyrRepo.TilføjDyr(hund2);
            //dyrRepo.TilføjDyr(hund3);

            //brugerRepo.OpretMedarbejder(medarbejder1);
            //brugerRepo.OpretMedarbejder(medarbejder2);
            //brugerRepo.OpretMedarbejder(medarbejder3);
            //dyrRepo.TilføjDyr(kat1);
            //dyrRepo.TilføjDyr(kat2);
            //dyrRepo.TilføjDyr(kat3);
            ////Console.WriteLine(kat1.PrintAltInfo());
            ////Console.WriteLine(kat1.Lavlyd());



            brugerRepo.VisBrugerInfo(1);

            brugerRepo.VisBrugerInfo(2);

            brugerRepo.VisBrugerInfo(3);

            //Skaber Mellemrum
            Console.WriteLine();
            //Oprettelse af brugere (Både kunder og medarbejdere), både som objekt i egen klasse og i brugerRepo.




            Console.WriteLine("Søger dyr frem:");

            dyrRepo.VisDyrInfo(123);
            dyrRepo.VisDyrInfo(456);
            dyrRepo.VisDyrInfo(1);
            dyrRepo.VisDyrInfo(2);
            dyrRepo.VisDyrInfo(6);

            dyrRepo.GetDyrByKøn("han");

            dyrRepo.GetDyrByKøn("hun");




            //Skaber Mellemrum
            Console.WriteLine();

            //Console.WriteLine(hund3.PrintAltInfo());
            //Console.WriteLine(hund3.Lavlyd());




            //Dette bruges til at fange og håndtere fejl -fx forsger man her, at tilføje en kunde med et ID som allreede findes
            try
            {
                brugerRepo.OpretKunde(kunde1);
                brugerRepo.OpretKunde(kunde2);

                besøgRepo.AddBesøg(besøg1);



                //Lav det som en kommentar og prøv at kør den. Fixed, kør!  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl, dette ID eksisterer allerede til en kunde/medarbejder:");
            }

            #region Switch menu
            {

                Console.WriteLine("Velkommen til DyreAdoptionsSystemet!");
                Console.WriteLine("Vælg din rolle:");
                Console.WriteLine("1. Gæst");
                Console.WriteLine("2. Kunde");
                Console.WriteLine("3. Medarbejder");

                string rolleValg = Console.ReadLine();

                switch (rolleValg)
                {
                    case "1": // Gæst
                        Console.WriteLine("Opret ny konto");
                        Console.Write("Indtast navn: ");
                        string navn = Console.ReadLine();
                        Console.Write("Indtast mail: ");
                        string mail = Console.ReadLine();
                        Console.Write("Indtast adresse: ");
                        string adresse = Console.ReadLine();
                        Console.Write("Indtast telefonnummer: ");
                        string telefon = Console.ReadLine();

                        Console.WriteLine($"Tak {navn}, din konto er oprettet!");
                        break;

                    case "2": // Kunde
                        Console.WriteLine("Hvad vil du gerne gøre?");
                        Console.WriteLine("1. Se alle dyr");
                        Console.WriteLine("2. Filtrér dyr");
                        Console.WriteLine("3. Book besøg");

                        string kundeValg = Console.ReadLine();
                        switch (kundeValg)
                        {
                            case "1":
                                Console.WriteLine("Her er alle dyr til adoption...");
                                // Her vises en liste over dyr
                                break;
                            case "2":
                                Console.WriteLine("vælg dyr der skal vises");
                                Console.WriteLine("1. Hund");
                                Console.WriteLine("2. Kat");
                                Console.WriteLine("3. Han");
                                Console.WriteLine("4. Hun");

                                // Det der skrives ind bliver nedenfor sikret er et tal, laves med int.Parse - det sikre at der ikke kan skrives bogstaver ind
                                int filtrering = int.Parse(Console.ReadLine());

                                //Her parser (konverterer) int til en string, fordi at readline kun tager tekst som input, det er for at sikre sig, at en tekst string kan tage int 
                                Console.WriteLine($"Du har valgt {filtrering.ToString()}.");

                                dyrRepo.ValgteDyr(filtrering);

                                // Tilføjelse af filtreringslogik
                                break;
                            case "3":
                                Console.Write("Vælg dato (dd-mm-åååå): ");
                                string dato = Console.ReadLine();
                                Console.Write("Vælg tidspunkt (fx 14:00): ");
                                string tid = Console.ReadLine();
                                Console.Write("Vælg hvilket dyr du vil besøge: ");
                                string dyrNavn = Console.ReadLine();

                                Console.WriteLine($"Du har booket et besøg med {dyrNavn} den {dato} kl. {tid}");
                                break;
                            default:
                                Console.WriteLine("Ugyldigt valg.");
                                break;
                        }
                        break;

                    case "3": // Medarbejder
                        Console.WriteLine("Medarbejdermenu:");
                        Console.WriteLine("1. Opret nyt dyr");
                        Console.WriteLine("2. Rediger oplysninger om dyr");
                        Console.WriteLine("3. Se kundeoplysninger");
                        Console.WriteLine("4. Vis alle besøg");

                        string medarbejderValg = Console.ReadLine();
                        switch (medarbejderValg)
                        {
                            case "1":
                                Console.Write("Indtast navn på dyret: ");
                                string dyrenavn = Console.ReadLine();
                                Console.Write("Indtast race: ");
                                string race = Console.ReadLine();
                                Console.Write("Indtast chipnummer: ");
                                string chip = Console.ReadLine();

                                Console.WriteLine($"{dyrenavn} er nu oprettet som 'ledig'.");
                                break;

                            case "2":
                                Console.Write("Indtast navn på dyret du vil redigere: ");
                                string redigerNavn = Console.ReadLine();
                                Console.Write("Opdateret alder: ");
                                string alder = Console.ReadLine();
                                Console.Write("Opdateret helbredstilstand: ");
                                string helbred = Console.ReadLine();

                                Console.WriteLine($"{redigerNavn}'s oplysninger er opdateret.");
                                break;

                            case "3":
                                Console.Write("Indtast kundens ID: ");
                                string kundeIDInput = Console.ReadLine();

                                if (int.TryParse(kundeIDInput, out int kundeID))
                                {
                                    Console.WriteLine($"Viser oplysninger for ID {kundeID}...");
                                    brugerRepo.VisBrugerInfo(kundeID);
                                }
                                else
                                {
                                    Console.WriteLine("Ugyldigt ID – det skal være et tal.");
                                }
                                break;
                            // Her kunne vises kontaktoplysninger og bookinger
                            case "4":
                                Console.WriteLine("Viser alle besøg:");
                                Console.WriteLine(besøgRepo.ToString());
                                break;


                            default:
                                Console.WriteLine("Ugyldigt valg.");
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("Ugyldig rolle valgt.");
                        break;
                }

            }
            foreach (Dyr dyr in dyrRepo.GetDyrByKøn("hun"))
            {
                Console.WriteLine(dyr.PrintAltInfo());
            }

        }

        #endregion
    }
}

