export class Discount {
  discountID: number;
  locationID: number;
  name: string;
  arabicName: string;
  description: string; 
  arabicDescription: string;
  fromTime: string;
  ToTime: string;
  statusID: number;
  image: string;    
  arabicImage: string;    
}
export class disclocationjunc {
  id: number;
  locationID: number;
  discountID: number;
}
