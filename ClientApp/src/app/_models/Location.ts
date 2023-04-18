export class Location {
  locationID: number;   
  amenitiesID:number;
  serviceID:number;  
  name: string;
  arabicName: string;
  arabicDescription:string;
  descripiton: string;
  address: string;
  Arabicaddress: string;
  contactNo: string;
  email: string;   
  minOrderAmount: number;
  longitude: string;
  latitude: string;   
  statusID: number;
  customerStatusID: number;
  landmarkID: number;   
  gmaplink: string;  
  imageURL: string;  
  lastUpdatedBy:string;                 
  lastUpdatedDate:string;
  isFeatured: number;
  locationImages:LocationImages[];
  imageSource:[];
  disabled: any;
}
export class LocationImages {
  locationID: number;
  image: string;

}
