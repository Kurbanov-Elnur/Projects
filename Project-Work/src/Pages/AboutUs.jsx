import React from 'react';

export default function AboutUs() {
  return (
    <section className="py-12 bg-gradient-to-r from-[#f0f4f8] to-[#e6eff1] sm:py-16 lg:py-20">
      <div className="px-4 mx-auto max-w-7xl sm:px-6 lg:px-8">
        <div className="text-center">
          <h2 className="text-3xl font-bold leading-tight text-gray-900 sm:text-4xl xl:text-5xl font-pj">
            About Us
          </h2>
          <p className="mt-4 text-base leading-7 text-gray-600 sm:mt-8 font-pj">
            We are dedicated to providing the best quality products and exceptional service to our customers.
          </p>
        </div>

        <div className="grid grid-cols-1 mt-10 text-center sm:mt-16 sm:grid-cols-2 sm:gap-x-12 gap-y-12 md:grid-cols-3 md:gap-0 xl:mt-24">
          {[
            {
              title: "Support",
              description: "Our support team is here to assist you with any questions or concerns. We are dedicated to ensuring your satisfaction.",
              svgPaths: [
                "M45 29V23C45 10.85 35.15 1 23 1C10.85 1 1 10.85 1 23V29",
                "M13 29H1V41C1 43.209 2.791 45 5 45H13V29Z",
                "M45 29H33V45H41C43.209 45 45 43.209 45 41V29Z"
              ]
            },
            {
              title: "Sales",
              description: "Our sales team is ready to help you find the perfect products for your needs. Contact us for personalized assistance.",
              svgPaths: [
                "M27 27H19V45H27V27Z",
                "M9 37H1V45H9V37Z",
                "M45 17H37V45H45V17Z",
                "M5 17L15 7L23 15L37 1",
                "M28 1H37V10"
              ]
            },
            {
              title: "Onboarding",
              description: "Our onboarding process ensures you get the best experience from the start. We guide you through every step.",
              svgPaths: [
                "M41 1H1V41H41V1Z",
                "M18 7H7V20H18V7Z",
                "M18 26H7V35H18V26Z",
                "M35 7H24V35H35V7Z"
              ]
            },
            {
              title: "Quality Assurance",
              description: "Our QA team ensures the highest standards for our products, delivering consistent and reliable quality.",
              svgPaths: [
                "M9.66667 25H6C3.23858 25 1 27.2386 1 30V37C1 39.7614 3.23858 42 6 42H36C38.7614 42 41 39.7614 41 37V30C41 27.2386 38.7614 25 36 25H31.8333C30.2685 25 29 26.2685 29 27.8333C29 29.3981 27.7315 30.6667 26.1667 30.6667H15.3333C13.7685 30.6667 12.5 29.3981 12.5 27.8333C12.5 26.2685 11.2315 25 9.66667 25Z",
                "M9 9H33",
                "M9 17H33",
                "M1 25H13V31H29V25H41",
                "M37 1H5C2.79086 1 1 2.79086 1 5V15C1 17.2091 2.79086 19 5 19H37C39.2091 19 41 17.2091 41 15V5C41 2.79086 39.2091 1 37 1Z"
              ]
            },
            {
              title: "Innovation",
              description: "We embrace innovation to stay ahead in the industry, continually improving our products and services.",
              svgPaths: [
                "M28 28H14L1 41H41L28 28Z",
                "M21 21C26.5228 21 31 16.5228 31 11C31 5.47715 26.5228 1 21 1C15.4772 1 11 5.47715 11 11C11 16.5228 15.4772 21 21 21Z"
              ]
            },
            {
              title: "Sustainability",
              description: "We are committed to sustainable practices, ensuring our operations have minimal impact on the environment.",
              svgPaths: [
                "M5 1L1 5V21.42L8.51 29.42L14.64 26.36L20.64 31L29.42 22.22L24.78 16.64L27.84 10.51L19.84 3H5Z",
                "M45 1H1V45H45V1Z"
              ]
            }
          ].map((item, index) => (
            <div
              key={index}
              className="cursor-pointer md:p-8 lg:p-14 md:border-l md:border-gray-200 hover:bg-[#41b482] hover:scale-105 transition-all duration-300 ease-in-out"
            >
              <svg
                className="mx-auto"
                width={index === 1 ? "46" : "42"}
                height={index === 1 ? "46" : "42"}
                viewBox="0 0 46 46"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                {item.svgPaths.map((path, i) => (
                  <path
                    key={i}
                    d={path}
                    fill={i === 0 ? "#D4D4D8" : "none"}
                    stroke="#004d40"
                    strokeWidth="2"
                    strokeMiterlimit="10"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                ))}
              </svg>
              <h3 className="mt-12 text-xl font-bold text-gray-900 font-pj">{item.title}</h3>
              <p className="mt-5 text-base text-gray-600 font-pj">{item.description}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}