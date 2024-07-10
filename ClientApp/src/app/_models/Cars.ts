export class Cars{
  carID: number;
  rowID: number;
  customerID: number;
  name: string;
  description: string;
  vinNo: string;
  makeID: number;
  modelID: number;
  year: number;
  color: string;
  registrationNo: string;
  checkLitre: number;
  engineType: string;
  recommendedAmount: string;
  statusID: number;
  binaryImage: string;
  lastUpdatedBy: string;
  lastUpdatedDate: string;
  createdOn: string;
  createdBy: string;
  locationID: number;
  userID: number;
  gender: string;
  carType: string;
}

export class CarsImages {
  id: number;
  carID: number;
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
