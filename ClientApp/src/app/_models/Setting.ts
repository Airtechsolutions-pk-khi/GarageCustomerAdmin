export class setting {
  id: number;
  locationID: number;
  title: string;
  description: string; 
  arabicTitle: string;
  arabicDescription: string;  
  pageName: string;  
  type:string;
  displayOrder: number;
  statusID: number;
  image: string;    
  alternateImage: string;    
}
export class locationjunc {
  id: number;
  locationID: number;
  settingID: number;
}
