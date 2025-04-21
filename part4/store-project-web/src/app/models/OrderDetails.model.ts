import { Product } from "./Product.model";

export interface OrderDetails{
    orderDetailsId?:number;
    productId?:number;
    quantity?:number;
    price?:number;
    totalPrice?:number
}