// app/page.tsx
"use client";

import { useEffect, useState } from "react";
import CallToAction from "@/app/components/Home/CallToAction";
import Footer from "@/app/components/Home/Footer";
import Hero from "@/app/components/Home/Hero";
import ProductsSection from "@/app/components/Home/ProductsSection";
import ServicesSection from "@/app/components/Home/ServicesSection";
import Navbar from "@/app/components/NavBar/Navbar";
import WhereToFindUs from "./components/Home/WhereToFindUs";
import FadeInDiv from "./components/FadeInDiv";
import IntroScreen from "./components/IntroScreen"; 

export default function HomePage() {
  const [showIntro, setShowIntro] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShowIntro(false);
    }, 2000);

    return () => clearTimeout(timer);
  }, []);

  if (showIntro) {
    return <IntroScreen />;
  }

  return (
    <div className="font-sans text-gray-800 antialiased">
      <Navbar />
      <main className="pt-20">
        <FadeInDiv>
          <Hero />
        </FadeInDiv>
        <FadeInDiv delay={200}>
          <ServicesSection />
        </FadeInDiv>
        <FadeInDiv delay={400}>
          <ProductsSection />
        </FadeInDiv>
        <FadeInDiv delay={600}>
          <WhereToFindUs />
        </FadeInDiv>
        <FadeInDiv delay={800}>
          <CallToAction />
        </FadeInDiv>
      </main>
      <Footer />
    </div>
  );
}