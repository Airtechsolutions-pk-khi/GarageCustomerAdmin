export class CarSells {
  carSellID:number;
  customerID:number;
  name:string;
  description:string;
  registrationNo: string;
  bodyTypeID: number;
  bodyType:string;
  bodyName:string;
  features:string;
  fuelType:string;
  engineType:string;
  kilometer:string;
  year:string;
  makeID:number;
  modelID:number;
  makeName:string;
  modelName:string;
  transmition:string;
  price:number;
  isInspected:boolean;
  cityID:number;
  countryCode:string;
  address:string;
  carSellAddID:number;
  bodyColor:string;
  assembly:string;
  reason:string;
  statusID:number;
  createdBy:number;
  createdDate:string;
  updatedBy:number;   
}
export class CarSellImages {
  id: number;
  carSellID: number;
  image: string;
  statusID: number;

}
export class Customers {
  customerID: number;
  fullName: string;
  email: string;
  mobile: string;
  city: string;
  password: string;
  image: string;
  locationID: number;
  brandID: number;
  statusID: number;
}

