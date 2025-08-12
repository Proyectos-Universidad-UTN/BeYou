"use client";

import { Container, Button } from "@mui/material";
import { useRouter } from "next/navigation";
import Image from "next/image";
import { ROUTES } from "@/navigation/Routes";

const products = [
  {
    id: 1,
    name: "Kit de Uñas Deluxe",
    description: "Todo lo que necesitas para un look profesional en casa.",
    price: "₡ 15000.00",
    imageUrl: "/assets/producto1.jpg",
  },
  {
    id: 2,
    name: "Esmalte de Larga Duración",
    description: "Colores vibrantes que resisten el paso del tiempo.",
    price: "₡ 2000.00",
    imageUrl: "/assets/producto2.jpg",
  },
  {
    id: 3,
    name: "Crema Hidratante",
    description: "Nutre e hidrata tu piel con nuestra fórmula exclusiva.",
    price: "₡ 2500.00",
    imageUrl: "/assets/producto3.jpg",
  },
];

export default function ProductsSection() {
  const router = useRouter();

  return (
    <section id="productos" className="py-20 bg-gray-100">
      <Container maxWidth="lg" className="text-center">
        <h2 className="text-3xl md:text-4xl font-bold mb-4 text-[#523249]">
          Productos Destacados
        </h2>
        <p className="text-gray-600 mb-12 max-w-2xl mx-auto">
          Descubre nuestra línea de productos exclusivos, diseñados para el
          cuidado personal.
        </p>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {products.map((product) => (
            <div
              key={product.id}
              className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-xl transition-shadow duration-300"
            >
              <div className="relative w-full overflow-hidden">
                <Image
                  src={product.imageUrl}
                  alt={product.name}
                  width={400}
                  height={300}
                  style={{
                    objectFit: "contain",
                    transition: "transform 0.3s ease",
                  }}
                  className="hover:scale-110"
                />
              </div>
              <div className="p-6 text-left">
                <h3 className="text-xl font-semibold text-[#523249]">
                  {product.name}
                </h3>
                <p className="text-gray-500 text-sm mt-2">
                  {product.description}
                </p>
                <div className="flex justify-between items-center mt-4">
                  <span className="text-2xl font-bold text-purple-600">
                    {product.price}
                  </span>
                 
                </div>
              </div>
            </div>
          ))}
        </div>
      </Container>
    </section>
  );
}
