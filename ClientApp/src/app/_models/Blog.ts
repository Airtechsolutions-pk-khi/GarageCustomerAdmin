export class Blog {
  blogID:number;
  title:string;
  arabicTitle:string;
  description:string;
  arabicDescription: string;
  startDate: string;
  endDate: string;
  country: string;
  city:number;
  type:string;
  statusID: number;
  isFeatured: boolean;
}
export class BlogImages_Junc {
  blogImageID: number;
  blogID: number;
  image: string;
  statusID: number;
}
export class Country {
  code: string;
  name: string;
}
export class City {
  id: number;
  name: string;
}
