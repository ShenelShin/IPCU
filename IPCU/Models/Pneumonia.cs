using System.Security.Policy;
using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class Pneumonia
    {
        public int Id { get; set; } // Primary key

        public string Fname { get; set; } // First Name
        public string Lname { get; set; } // Last Name
        public string Mname { get; set; } // Middle Name
        public string HospitalNumber { get; set; } // Foreign Key to Hospital Records
        public DateTime DateOfBirth { get; set; } // Date of Birth
        public int Age { get; set; } // Age
        public string UnitWardArea { get; set; } // Unit/Ward/Area
        public string MainService { get; set; } // Main Service
        public DateTime DateOfEvent { get; set; } // Date of Event
        public string Investigator { get; set; } // Investigator
        public DateTime DateOfAdmission { get; set; } // Date of Admission

        // Disposition Information
        public string Disposition { get; set; } // Mortality, Discharged, Still Admitted, Transferred In, Transferred Out
        public DateTime DispositionDate { get; set; } // Date of Disposition
        public string? DispositionTransfer { get; set; } // Transfer Details

        // Additional Patient Detailsffff
        public string Gender { get; set; } // Male, Femalef
        public string Classification { get; set; } // Pay, Servicef
        public string MDRO { get; set; } // Yes, No
        public string TypeClass { get; set; } 
        public string? PNEU_Subclass { get; set; } 

        // lahat neto meron
        public string? PersistentorProgressive1_1 { get; set; }

        // Section PNEU1_1: Additional Symptoms (At least one)
        public bool Fever1_1 { get; set; }
        public bool Leukopenia1_1 { get; set; } 
        public bool Leukocytosis1_1 { get; set; }
        public bool Adults70old1_1 { get; set; }
        //at least one (2) of the following
        public bool NewPurulentSputum1_1 { get; set; } 
        public bool WorseningCoughOrDyspnea1_1 { get; set; }
        public bool RalesOrBronchialBreathSounds1_1 { get; set; } 
        public bool WorseningGasExchange1_1 { get; set; }



        // Section PNEU1_2
        public string? PersistentorProgressive1_2 { get; set; }


        // Additional Criteria

        public bool WorseningGasExchange1_2 { get; set; } 

        // At least three (3) required
        public bool TemperatureInstability1_2 { get; set; } 
        public bool LeukopeniaOrLeukocytosis1_2 { get; set; }
        public bool NewPurulentSputum1_2 { get; set; } 
        public bool ApneaOrTachypneaOrNasalFlaring1_2 { get; set; } 
        public bool WheezingRalesOrRhonchi1_2 { get; set; }
        public bool Cough1_2 { get; set; } 
        public bool BradycardiaOrTachycardia1_2 { get; set; }

        // Section PNEU1_3:
        public string? PersistentorProgressive1_3 { get; set; }


        // At least three (3) of the following
        public bool Fever1_3 { get; set; } // Fever (> 38.0℃)
        public bool Leukopenia1_3 { get; set; } // ≤ 4,000 WBC/mm3
        public bool NewPurulentSputum1_3 { get; set; }
        public bool NewWorseningCough1_3 { get; set; }
        public bool RalesorBronchial1_3 { get; set; }
        public bool WorseningGas1_3 { get; set; }

        // =====================
        // Section PNEU2_1
        // =====================
        public string? PersistentorProgressive2_1 { get; set; }


        // At least 1 of the following
        public bool Fever2_1 { get; set; } // Fever (> 38.0℃)
        public bool Leukopenia12_1 { get; set; } // ≤ 4,000 WBC/mm3
        public bool Leukocytosis2_1 { get; set; } // ≥ 12,000 WBC/mm3
        public bool Adults70old2_1 { get; set; } // Adults ≥ 70 years old with no other recognized cause

        // At least 1 of the following
        public bool NewPurulentSputum2_1 { get; set; } // New onset of purulent sputum or change in character of sputum
        public bool WorseningCoughOrDyspnea2_1 { get; set; } // New onset or worsening cough, dyspnea, or tachypnea
        public bool RalesOrBronchialBreathSounds2_1 { get; set; } // Rales or bronchial breath sounds
        public bool WorseningGasExchange2_1 { get; set; } // Worsening gas exchange (e.g., O₂ requirement)

        // AND, at least one (1) of the following:
        public bool OrganismIdentifiedFromBlood_2_1 { get; set; } // Organism identified from blood
        public bool PositiveQuantitativeCultureLRT_2_1 { get; set; } // Positive quantitative culture from minimally contaminated LRT specimen (BAL, brushing, aspirate)
        public bool BALCellsContainIntracellularBacteria_2_1 { get; set; } // ≥5% BAL-obtained cells contain intracellular bacteria
        public bool PositiveQuantitativeCultureLungTissue_2_1 { get; set; } // Positive quantitative culture from lung tissue

        // Histopathologic exam findings
        public bool HistopathologicExam2_1 { get; set; } // Positive quantitative culture from lung tissue

        public bool AbscessFormationOrFoci_2_1 { get; set; } // Abscess formation or foci of consolidation with intense PMN accumulation
        public bool EvidenceOfFungalInvasion_2_1 { get; set; } // Evidence of lung parenchyma invasion by fungal hyphae or pseudohyphae

        // Culture
        public DateTime? CultureDate2_1 { get; set; }
        public string? CultureResults2_1 { get; set; }

        // =====================
        // Section PNEU2_2
        // =====================

        public string? PersistentorProgressive2_2 { get; set; }

        // At least 1 of the following
        public bool Fever2_2 { get; set; } // Fever (> 38.0℃)
        public bool Leukopenia12_2 { get; set; } // ≤ 4,000 WBC/mm3
        public bool Leukocytosis2_2 { get; set; } // ≥ 12,000 WBC/mm3
        public bool Adults70old2_2 { get; set; } // Adults ≥ 70 years old with no other recognized cause

        // At least 1 of the following
        public bool NewPurulentSputum2_2 { get; set; } // New onset of purulent sputum or change in character of sputum
        public bool WorseningCoughOrDyspnea2_2 { get; set; } // New onset or worsening cough, dyspnea, or tachypnea
        public bool RalesOrBronchialBreathSounds2_2 { get; set; } // Rales or bronchial breath sounds
        public bool WorseningGasExchange2_2 { get; set; } // Worsening gas exchange (e.g., increased O₂ requirement)

        // At least 1 of the following
        public bool VirusBordetellaLegionella2_2 { get; set; } // Virus, Bordetella, or Legionella detected
        public bool FourfoldrisePaired2_2 { get; set; } // Fourfold rise in paired serum samples
        public bool FourfoldriseLegionella2_2 { get; set; } // Fourfold rise in Legionella pneumophila antibody titer
        public bool DetectionofLegionella2_2 { get; set; } // Detection of Legionella pneumophila by culture or urinary antigen

        // Culture
        public DateTime? CultureDate2_2 { get; set; }
        public string? CultureResults2_2 { get; set; }


        // Section PNEU3: 
        public string? PersistentorProgressive3 { get; set; }

        // At least 1 of the following
        public bool Fever3 { get; set; } // Fever (> 38.0℃)
        public bool Adults70old3 { get; set; } // Adults ≥ 70 years old with no other recognized cause
        public bool NewPurulentSputum3 { get; set; } // New onset of purulent sputum or change in character of sputum
        public bool WorseningCoughOrDyspnea3 { get; set; } // New onset or worsening cough, dyspnea, or tachypnea
        public bool RalesOrBronchialBreathSounds3 { get; set; } // Rales or bronchial breath sounds
        public bool WorseningGasExchange3 { get; set; }
        public bool Hemoptysis3 { get; set; }
        public bool PleuriticChestPain3 { get; set; }

        // At least 1 of the following
        public bool IdentificationCandida3 { get; set; }
        public bool EvidenceofFungi3 { get; set; }
        public bool DirectMicroscopicExam3 { get; set; }
        public bool PositiveCultureFungi3 { get; set; }
        public bool NonCultureDiagnostic3 { get; set; }


    }
}