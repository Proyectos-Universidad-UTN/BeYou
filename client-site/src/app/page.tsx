"use client";

import CallToAction from "@/app/components/Home/CallToAction";
import Footer from "@/app/components/Home/Footer";
import Hero from "@/app/components/Home/Hero";
import ProductsSection from "@/app/components/Home/ProductsSection";
import ServicesSection from "@/app/components/Home/ServicesSection";
import Navbar from "@/app/components/NavBar/Navbar";
import WhereToFindUs from "./components/Home/WhereToFindUs";

export default function HomePage() {
  return (
    <div className="font-sans text-gray-800 bg-gray-50 antialiased">
      <Navbar />
      <main className="pt-20">
        <Hero />
        <ServicesSection />
        <ProductsSection />
        <WhereToFindUs />
        <CallToAction />
      </main>
      <Footer />
    </div>
  );
}
