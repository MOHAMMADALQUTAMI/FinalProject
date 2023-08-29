import { Component } from '@angular/core';

@Component({
  selector: 'app-myproducts',
  templateUrl: './myproducts.component.html',
  styleUrls: ['./myproducts.component.scss']
})
export class MyproductsComponent {
  products = [
    {
      title: 'Product 1',
      image: 'house_default.png',
      price: '$19.99'
    },
    {
      title: 'Product 2',
      image: 'house_default.png',
      price: '$29.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },

  ];
}
