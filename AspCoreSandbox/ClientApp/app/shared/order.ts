
/* podobno lodash to taki Linq dla ts */
import * as _ from "lodash";

export class Order {
    orderId: number;
    orderDate: Date = new Date();
    orderNumber: string;
    items: Array<OrderItem> = new Array<OrderItem>();

    /* sam get oznacza, że jest read-only */
    get subtotal(): number { return _.sum(_.map(this.items, i => i.unitPrice * i.quantity)) };
}


export class OrderItem {
    id: number;
    productId: number;
    quantity: number;
    unitPrice: number;
}
