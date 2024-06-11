export class Location {
  locationID: number;   
  userID: number;   
  amenitiesID:number;
  cityID:number;
  countryCode:string;
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
  businessType: string;   
  statusID: number;
  customerStatusID: number;
  landmarkID: number;   
  gmaplink: string;  
  imageURL: string;  
  lastUpdatedBy:string;                 
  lastUpdatedDate:string;
  isFeatured: number;
  locationImages: LocationImages[];
  locationTiming: LocationTimings[];
  imageSource:[];
  disabled: any;
  image: string;
  artime: string;
}
export class LocationImages {
  locationID: number;
  image: string;

}
export class LocationTimings {
  locationID: number;
  name: string;
  time: string;
  aName: string;
  aTime: string;
}
