import { OrderDetails } from "./OrderDetails.model";
import { Supplier } from "./Supplier.model";

export interface Order{
    Id?:Number;
    SupplierId?:Number;
    Status?:string;
    TotalPrice?:Number;
    OrderDetails?:OrderDetails[];
}