using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Xml.Schema;

namespace TPMoyennes
{

    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count(); ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count(); matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.Moyenne(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.Moyenne() + "\n");
            //Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.Moyenne(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.Moyenne() + "\n");
            Console.Read();

        }
    }
}

// Classes fournies par HNI Institut
class Note
{
    public int matiere { get; private set; }
    public float note { get; private set; }
    public Note(int m, float n)
    {
        matiere = m;
        note = n;
    }
}

// Mes classes
class Classe
{
    public string nomClasse;
    public string[] matieres = { };
    public Eleve[] eleves = new Eleve[9];
    public int nbEleve = 0;
    public int matiere;


    public Classe(string nomClasse)
    {
        this.nomClasse = nomClasse;
    }
    //Méthode ajout
    public void ajouterEleve(string fName, string lName)
    {
        this.eleves[nbEleve] = new Eleve(fName, lName);
        this.nbEleve += 1;
    }
    //Méthode ajout de matiere
    public void ajouterMatiere(string nomMatiere)
    {
        List<string> list = new List<string>(this.matieres.ToList());
        list.Add(nomMatiere);
        this.matieres = list.ToArray();
    }

    // Méthode Moyenne de la classe dans une matière
    public double Moyenne(int _matiere)
    {
        float totalNotesEleve = 0;
        float moyenneEleve;
        float totalMoyenne = 0;
        float moyenneClasse;
        this.matiere = _matiere;

        foreach (var e in eleves)
        {
            foreach (var f in e.notes)
            {
                if (f.matiere == this.matiere)
                    totalNotesEleve += f.note;
            }
            moyenneEleve = totalNotesEleve / 5;
            totalNotesEleve = 0;
            totalMoyenne += moyenneEleve;
        }

        moyenneClasse = totalMoyenne / eleves.Length;

        return Math.Round(moyenneClasse, 2);
    }
    // Méthode Moyenne générale de la classe
    public double Moyenne()
    {
        float totalNotesEleve = 0;
        float moyenneEleve;
        float totalMoyenne = 0;
        float moyenneClasse;

        foreach (var e in eleves)
        {
            foreach (var f in e.notes)
            {
                totalNotesEleve += f.note;
            }
            moyenneEleve = totalNotesEleve / 20;
            totalNotesEleve = 0;
            totalMoyenne += moyenneEleve;
        }
        moyenneClasse = totalMoyenne / eleves.Length;

        return Math.Round(moyenneClasse, 2);
    }
}

class Eleve
{
    public string prenom { get; set; }
    public string nom { get; set; }
    public Note[] notes = new Note[20];
    public int nbNotes = 0;
    public int matiere;

    public Eleve(string fName, string lName)
    {
        prenom = fName;
        nom = lName;
    }

    // Méthode ajout d'une note
    public void ajouterNote(Note fNote)
    {
        this.notes[nbNotes] = fNote;
        this.nbNotes += 1;
    }

    // Méthode Moyenne d'un élève dans une matière
    public double Moyenne(int _matiere)
    {
        float totalNotes = 0;
        float moyenneEleve;
        this.matiere = _matiere;
        int notesCount = 0;
        foreach (var e in notes)
        {
            if (e.matiere == this.matiere)
            {
                totalNotes += e.note;
                notesCount += 1;
            }
        }
        //Fonctionne même si on veut ajouter plus ou moins que 5 notes
        moyenneEleve = totalNotes / notesCount;

        //moyenneEleve = totalNotes / 5;
        //Plus clair mais ne marche quand si il y a 5 notes comme dans l'exemple'
        return Math.Round(moyenneEleve, 2);
    }

    // Méthode Moyenne générale d'un élève
    public double Moyenne()
    {
        float totalNotes = 0;
        float moyenneEleve;
        int notesCount = 0;
        {
            foreach (var e in notes)
            {
                totalNotes += e.note;
                notesCount += 1;
            }
            moyenneEleve = totalNotes / notesCount;
            return Math.Round(moyenneEleve, 2);
        }
    }
}



