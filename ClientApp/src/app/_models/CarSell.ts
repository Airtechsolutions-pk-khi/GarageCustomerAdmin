export class CarSells {
  carSellID:number;
  customerID:number;
  name:string;
  description:string;
  registrationNo:string;
  bodyType:string;
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
  CountryCode:string;
  address:string;
  carSellAddID:number;
  bodyColor:string;
  assembly:string;
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

