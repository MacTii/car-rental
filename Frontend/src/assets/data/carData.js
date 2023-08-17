// import all images from assets/images directory
import img01 from "../all-images/cars-img/tesla-malibu.png";
import img02 from "../all-images/cars-img/ford-fiesta.png";
import img03 from "../all-images/cars-img/bmw-offer.png";
import img04 from "../all-images/cars-img/nissan-offer.png";
import img05 from "../all-images/cars-img/offer-toyota.png";
import img06 from "../all-images/cars-img/mercedes-offer.png";
import img07 from "../all-images/cars-img/rolls-royce-ghost.png";
import img08 from "../all-images/cars-img/lamborghini-aventador.png";

const carData = [
  {
    id: 1,
    make: "Tesla",
    rating: 112,
    carName: "Tesla Malibu",
    imgUrl: img01,
    model: "Model 3",
    price: 50,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "The Tesla Malibu is an electric car that combines cutting-edge technology with sustainability. It offers GPS navigation, heated seats, and automatic transmission, providing a luxurious and eco-friendly driving experience.",
  },

  {
    id: 2,
    make: "Lamborghini",
    rating: 152,
    carName: "Lamborghini Aventador",
    imgUrl: img08,
    model: "Model-2022",
    price: 80,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "Luxury and power combined in the stunning Lamborghini Aventador. With its sleek design and exhilarating performance, this supercar is sure to turn heads. Experience the thrill of driving with unmatched style and speed.",
  },

  {
    id: 3,
    make: "BMW",
    rating: 132,
    carName: "BMW X3",
    imgUrl: img03,
    model: "Model-2022",
    price: 65,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "The BMW X3 combines elegance, versatility, and cutting-edge technology. With its spacious interior, advanced features, and impressive performance, this luxury SUV is designed to enhance every journey. Discover the perfect blend of comfort and driving pleasure.",
  },

  {
    id: 4,
    make: "Nissan",
    rating: 102,
    carName: "Nissan Maxima",
    imgUrl: img04,
    model: "Model-2022",
    price: 70,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "The Nissan Maxima is a stylish sedan that offers a perfect balance of sophistication and sportiness. With its powerful engine, upscale interior, and advanced safety features, the Maxima delivers a thrilling driving experience with comfort and peace of mind.",
  },

  {
    id: 5,
    make: "Toyota",
    rating: 94,
    carName: "Toyota Camry",
    imgUrl: img05,
    model: "Model-2022",
    price: 45,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "The Toyota Camry is a reliable and refined midsize sedan that excels in both performance and comfort. With its fuel-efficient engine, spacious cabin, and advanced safety technology, the Camry is a practical choice for everyday driving. Experience a smooth and enjoyable ride in this dependable and feature-packed vehicle.",
  },

  {
    id: 6,
    make: "Mercedes",
    rating: 119,
    carName: "Mercedes Benz XC90",
    imgUrl: img06,
    model: "Model-2022",
    price: 85,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "Mercedes Benz XC90 is a luxurious and powerful SUV with a sleek design, advanced features, and spacious interior. It offers GPS navigation, heated seats, and automatic transmission for convenience and comfort.",
  },

  {
    id: 7,
    make: "Ford",
    rating: 82,
    carName: "Ford Fiesta",
    imgUrl: img02,
    model: "Model 3",
    price: 50,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "Ford Fiesta is a dynamic and stylish car with a sporty appearance, comfortable interior, and advanced multimedia system. It features GPS navigation, heated seats, and automatic transmission for unforgettable driving experiences.",
  },

  {
    id: 8,
    make: "Rolls Royce",
    rating: 52,
    carName: "Rolls Royce Ghost",
    imgUrl: img07,
    model: "Model 3",
    price: 50,
    speed: "20kmpl",
    gps: "GPS Navigation",
    seatType: "Heated seats",
    automatic: "Automatic",
    description:
      "Rolls Royce Ghost is an iconic luxury car known for its elegant design, impeccable craftsmanship, and exceptional comfort. It incorporates the latest technologies and delivers a dynamic driving experience.",
  },
];

export default carData;
