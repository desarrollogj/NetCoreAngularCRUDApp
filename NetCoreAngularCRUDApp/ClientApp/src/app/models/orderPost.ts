import { OrderPostItem } from './orderPostItem';

export class OrderPost {
    orderId?: number;
    customerId: number;
    customerName: string;
    paymentType: string;
    date: Date;
    items: OrderPostItem[];
}
