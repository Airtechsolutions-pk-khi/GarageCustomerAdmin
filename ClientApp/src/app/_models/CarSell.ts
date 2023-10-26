export class CarSells {
  carSellID:number;
  customerID:number;
  customerPhone:string;
  name:string;
  description:string;
  registrationNo: string;
  bodyTypeID: number;
  bodyType:string;
  features:string;
  fuelType:string;
  engineType:string;
  kilometer:number;
  year:string;
  makeID:number;
  modelID:number;
  makeName:string;
  modelName:string;
  transmition:string;
  price:number;
  isInspected:boolean;
  cityID:number;
  countryID:string;
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

export class Make {
  makeID: number;
  rowID: number;
  name: string;
  aArabicName: string;
  imagePath: string;
  lastUpdatedBy: string;
  lastUpdatedDate: string;
  statusID: number;
  createdOn: string;
  createdBy: string;
  displayOrder: number;
}
export class Models {
  modelID: number;
  rowID: number;
  makeID: number;
  name: string;
  arabicName: string;
  year: string;
  engineNo: string;
  recommendedLitres: string;
  imagePath: string;
  lastUpdatedBy: string;
  lastUpdatedDate: string;
  statusID: number;
  createdOn: string;
  createdBy: string;
}
export class Country {
  code: string;
  name: string;
}
export class City {
  id: number;
  name: string;
}
export class Feature {
  featureID: number;
  name: string;
}
