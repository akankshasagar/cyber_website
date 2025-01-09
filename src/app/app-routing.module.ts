import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { CoursesComponent } from './courses/courses.component';
import { SigninComponent } from './homepage/signin/signin.component';
import { SignupComponent } from './homepage/signup/signup.component';
import { CS001Component } from './courses/cs001/cs001.component';
import { AuthGuard } from './guards/auth.guard';
import { AllcoursesComponent } from './allcourses/allcourses.component';
import { InformationSecurityComponent } from './courses/cs001/information-security-and-cia-triad/information-security/information-security.component';
import { CiaTriadComponent } from './courses/cs001/information-security-and-cia-triad/cia-triad/cia-triad.component';
import { CyberThreatComponent } from './courses/cs001/information-security-and-cia-triad/cyber-threat/cyber-threat.component';
import { CyberVulnerabilityComponent } from './courses/cs001/information-security-and-cia-triad/cyber-vulnerability/cyber-vulnerability.component';
import { CyberRiskComponent } from './courses/cs001/information-security-and-cia-triad/cyber-risk/cyber-risk.component';
import { ProtectNetworkComponent } from './courses/cs001/information-security-and-cia-triad/protect-network/protect-network.component';
import { AttackVectorsComponent } from './courses/cs001/attack-surfaces-and-its-types/attack-vectors/attack-vectors.component';
import { AttackSurfacesComponent } from './courses/cs001/attack-surfaces-and-its-types/attack-surfaces/attack-surfaces.component';
import { TypesOfAttackSurfacesComponent } from './courses/cs001/attack-surfaces-and-its-types/types-of-attack-surfaces/types-of-attack-surfaces.component';
import { AttackSurfacesLimitedComponent } from './courses/cs001/attack-surfaces-and-its-types/attack-surfaces-limited/attack-surfaces-limited.component';
import { PhishingComponent } from './courses/cs001/phishing-spoofing-and-its-types/phishing/phishing.component';
import { SpoofingComponent } from './courses/cs001/phishing-spoofing-and-its-types/spoofing/spoofing.component';
import { RansomwareComponent } from './courses/cs001/phishing-spoofing-and-its-types/ransomware/ransomware.component';
import { WifiSecurityComponent } from './courses/cs001/phishing-spoofing-and-its-types/wifi-security/wifi-security.component';
import { AttacksOnWirelessEnvironmentComponent } from './courses/cs001/wireless-environment-scams-and-precautions/attacks-on-wireless-environment/attacks-on-wireless-environment.component';
import { SecureWirelessCommunicationsComponent } from './courses/cs001/wireless-environment-scams-and-precautions/secure-wireless-communications/secure-wireless-communications.component';
import { ScamsComponent } from './courses/cs001/wireless-environment-scams-and-precautions/scams/scams.component';
import { PrecautionsComponent } from './courses/cs001/wireless-environment-scams-and-precautions/precautions/precautions.component';
import { DesktopSecurityComponent } from './courses/cs001/wireless-environment-scams-and-precautions/desktop-security/desktop-security.component';
import { PracticesToFollowComponent } from './courses/cs001/dos-and-donts/practices-to-follow/practices-to-follow.component';
import { DosComponent } from './courses/cs001/dos-and-donts/dos/dos.component';
import { DontsComponent } from './courses/cs001/dos-and-donts/donts/donts.component';
import { IncidentResponseManagementComponent } from './courses/cs001/ir-management/incident-response-management/incident-response-management.component';
import { PhasesOfIncidentResponseManagementComponent } from './courses/cs001/ir-management/phases-of-incident-response-management/phases-of-incident-response-management.component';
import { RoleOfIncidentResponseManagerComponent } from './courses/cs001/ir-management/role-of-incident-response-manager/role-of-incident-response-manager.component';
import { DataProtectionComponent } from './courses/cs001/data/data-protection/data-protection.component';
import { DataProtectionMeasuresComponent } from './courses/cs001/data/data-protection-measures/data-protection-measures.component';
import { HackersGenerallyTargetComponent } from './courses/cs001/data/hackers-generally-target/hackers-generally-target.component';
import { ToolsAndTechnologiesComponent } from './courses/cs001/data/tools-and-technologies/tools-and-technologies.component';
import { DnsAttacksComponent } from './courses/cs001/dns-and-apt-attacks/dns-attacks/dns-attacks.component';
import { AptAttacksComponent } from './courses/cs001/dns-and-apt-attacks/apt-attacks/apt-attacks.component';
import { SqlInjectionComponent } from './courses/cs001/dns-and-apt-attacks/sql-injection/sql-injection.component';
import { AttackersExploitationComponent } from './courses/cs001/dns-and-apt-attacks/attackers-exploitation/attackers-exploitation.component';
import { ManInTheMiddleAttackComponent } from './courses/cs001/dns-and-apt-attacks/man-in-the-middle-attack/man-in-the-middle-attack.component';
import { DdosComponent } from './courses/cs001/cyberstalking-and-cyberbullying/ddos/ddos.component';
import { CyberstalkingComponent } from './courses/cs001/cyberstalking-and-cyberbullying/cyberstalking/cyberstalking.component';
import { CyberstalkingCyberbullyingPreventionComponent } from './courses/cs001/cyberstalking-and-cyberbullying/cyberstalking-cyberbullying-prevention/cyberstalking-cyberbullying-prevention.component';
import { IsactTest001Component } from './courses/cs001/information-security-and-cia-triad/isact-test001/isact-test001.component';
import { Attacksurfacestest001Component } from './courses/cs001/attack-surfaces-and-its-types/attacksurfacestest001/attacksurfacestest001.component';
import { WirelessEnvironmentTest001Component } from './courses/cs001/wireless-environment-scams-and-precautions/wireless-environment-test001/wireless-environment-test001.component';
import { Phishingspoofingtest001Component } from './courses/cs001/phishing-spoofing-and-its-types/phishingspoofingtest001/phishingspoofingtest001.component';
import { DosAndDontsTest001Component } from './courses/cs001/dos-and-donts/dos-and-donts-test001/dos-and-donts-test001.component';
import { IrManagementTest001Component } from './courses/cs001/ir-management/ir-management-test001/ir-management-test001.component';
import { DataProtectionTest001Component } from './courses/cs001/data/data-protection-test001/data-protection-test001.component';
import { DntAptTest001Component } from './courses/cs001/dns-and-apt-attacks/dnt-apt-test001/dnt-apt-test001.component';
import { CyberTest001Component } from './courses/cs001/cyberstalking-and-cyberbullying/cyber-test001/cyber-test001.component';
import { AdminpageComponent } from './adminpage/adminpage.component';
import { EditcourseComponent } from './adminpage/editcourse/editcourse.component';
import { AddcourseComponent } from './adminpage/addcourse/addcourse.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'homepage'},
  { path: 'homepage', component: HomepageComponent },
  { path: 'homepage/signin', component: SigninComponent },
  { path: 'homepage/signup', component: SignupComponent },
  { path: 'adminpage', component: AdminpageComponent, canActivate:[AuthGuard] },
  { path: 'adminpage/editcourse', component: EditcourseComponent, canActivate: [AuthGuard] },
  { path: 'adminpage/addcourse', component: AddcourseComponent, canActivate: [AuthGuard] },
    // { path: '', pathMatch: 'full', redirectTo: 'editcourse' },

    // { path: 'editcourse', component: EditcourseComponent }},
  { path: 'courses', component: CoursesComponent, canActivate:[AuthGuard] },
  { path: 'courses/cs001', component: CS001Component, canActivate:[AuthGuard], children: [
    { path: '', pathMatch: 'full', redirectTo: 'information-security-and-cia-triad/information-security' },



    // information-security-and-cia-triad
    { path: 'information-security-and-cia-triad/information-security', component: InformationSecurityComponent },
    { path: 'information-security-and-cia-triad/cia-triad', component: CiaTriadComponent },
    { path: 'information-security-and-cia-triad/cyber-threat', component: CyberThreatComponent },
    { path: 'information-security-and-cia-triad/cyber-vulnerability', component: CyberVulnerabilityComponent },
    { path: 'information-security-and-cia-triad/cyber-risk', component: CyberRiskComponent },
    { path: 'information-security-and-cia-triad/protect-network', component: ProtectNetworkComponent },
    { path: 'information-security-and-cia-triad/isact-test001', component: IsactTest001Component },


    // attack-surfaces-and-its-types
    { path: 'attack-surfaces-and-its-types/attack-vectors', component: AttackVectorsComponent },
    { path: 'attack-surfaces-and-its-types/attack-surfaces', component: AttackSurfacesComponent },
    { path: 'attack-surfaces-and-its-types/types-of-attack-surfaces', component: TypesOfAttackSurfacesComponent },
    { path: 'attack-surfaces-and-its-types/attack-surfaces-limited', component: AttackSurfacesLimitedComponent },
    { path: 'attack-surfaces-and-its-types/attacksurfacestest001', component: Attacksurfacestest001Component },    



    //phishing-spoofing-and-its-types
    { path: 'phishing-spoofing-and-its-types/phishing', component: PhishingComponent },
    { path: 'phishing-spoofing-and-its-types/spoofing', component: SpoofingComponent },
    { path: 'phishing-spoofing-and-its-types/ransomware', component: RansomwareComponent },
    { path: 'phishing-spoofing-and-its-types/wifi-security', component: WifiSecurityComponent},
    { path: 'phishing-spoofing-and-its-types/phishingspoofingtest001', component: Phishingspoofingtest001Component },


    //wireless-environment-scams-and-precautions
    { path: 'wireless-environment-scams-and-precautions/attacks-on-wireless-environment', component: AttacksOnWirelessEnvironmentComponent },
    { path: 'wireless-environment-scams-and-precautions/secure-wireless-communications', component: SecureWirelessCommunicationsComponent },
    { path: 'wireless-environment-scams-and-precautions/scams', component: ScamsComponent },
    { path: 'wireless-environment-scams-and-precautions/precautions', component: PrecautionsComponent },
    { path: 'wireless-environment-scams-and-precautions/desktop-security', component: DesktopSecurityComponent },
    { path: 'wireless-environment-scams-and-precautions/wireless-environment-test001', component: WirelessEnvironmentTest001Component },



    //dos-and-donts
    { path: 'dos-and-donts/practices-to-follow', component: PracticesToFollowComponent },
    { path: 'dos-and-donts/dos', component: DosComponent },
    { path: 'dos-and-donts/donts', component: DontsComponent },
    { path: 'dos-and-donts/dos-and-donts-test001', component: DosAndDontsTest001Component },



    //IR-management
    { path: 'IR-management/incident-response-management', component: IncidentResponseManagementComponent },
    { path: 'IR-management/phases-of-incident-response-management', component: PhasesOfIncidentResponseManagementComponent },
    { path: 'IR-management/role-of-incident-response-manager', component: RoleOfIncidentResponseManagerComponent },
    { path: 'IR-management/ir-management-test001', component: IrManagementTest001Component },


    
    //data
    { path: 'data/data-protection', component: DataProtectionComponent },
    { path: 'data/data-protection-measures', component: DataProtectionMeasuresComponent },
    { path: 'data/hackers-generally-target', component: HackersGenerallyTargetComponent },
    { path: 'data/tools-and-technologies', component: ToolsAndTechnologiesComponent },
    { path: 'data/data-protection-test001', component: DataProtectionTest001Component },



    //dns-and-apt-attacks
    { path: 'dns-and-apt-attacks/dns-attacks', component: DnsAttacksComponent },
    { path: 'dns-and-apt-attacks/apt-attacks', component: AptAttacksComponent },
    { path: 'dns-and-apt-attacks/sql-injection', component: SqlInjectionComponent },
    { path: 'dns-and-apt-attacks/attackers-exploitation', component: AttackersExploitationComponent },
    { path: 'dns-and-apt-attacks/man-in-the-middle-attack', component: ManInTheMiddleAttackComponent },
    { path: 'dns-and-apt-attacks/dnt-apt-test001', component: DntAptTest001Component },



    //cyberstalking-and-cyberbullying
    { path: 'cyberstalking-and-cyberbullying/ddos', component: DdosComponent },
    { path: 'cyberstalking-and-cyberbullying/cyberstalking', component: CyberstalkingComponent },
    // { path: 'cyberbullying', component: CyberbullyingComponent },
    { path: 'cyberstalking-and-cyberbullying/cyberstalking-cyberbullying-prevention', component: CyberstalkingCyberbullyingPreventionComponent },
    { path: 'cyberstalking-and-cyberbullying/cyber-test001', component: CyberTest001Component }

    
  ] },    
  { path: 'allcourses', component: AllcoursesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
