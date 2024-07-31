import React from 'react';

const images = [
    require('../Assets/Products/chanel.png'),
    require('../Assets/Products/macbook.png'),
    require('../Assets/Products/man-mix.png'),
    require('../Assets/Products/nike.png'),
    require('../Assets/Products/watch.png'),
    require('../Assets/Products/woman-mix.png'),
];

export default function News() {
    const newsItems = [
        {
            id: 1,
            title: "Introducing the Chanel Collection",
            date: "2024-01-15",
            description: "Explore our new Chanel collection featuring luxury and elegance. Shop now to find the perfect piece for you.",
            image: images[0]
        },
        {
            id: 2,
            title: "MacBook Sale - Limited Time Offer",
            date: "2024-03-10",
            description: "Get exclusive discounts on the latest MacBook models. Don't miss out on this limited time offer!",
            image: images[1]
        },
        {
            id: 3,
            title: "Stylish Men's Wear for the Season",
            date: "2024-04-05",
            description: "Check out our latest arrivals in men's fashion. Perfect for any occasion, stylish and comfortable.",
            image: images[2]
        },
        {
            id: 4,
            title: "Nike Collection Now Available",
            date: "2024-05-20",
            description: "Discover the newest Nike collection. From sportswear to casuals, find what fits your lifestyle.",
            image: images[3]
        },
        {
            id: 5,
            title: "New Watch Designs Released",
            date: "2024-06-15",
            description: "Explore our latest watch designs. Whether for fashion or functionality, find your ideal timepiece.",
            image: images[4]
        },
        {
            id: 6,
            title: "Exclusive Women's Fashion Arrivals",
            date: "2024-07-01",
            description: "Celebrate the season with our new women's fashion arrivals. Elegant, trendy, and perfect for any occasion.",
            image: images[5]
        }
    ];

    return (
        <div>
            <h3 className="text-gray-600 text-4xl font-medium m-5 text-center">News</h3>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 m-5">
                {[...newsItems].reverse().map(news => (
                    <div key={news.id} className="cursor-pointer bg-white border rounded-lg overflow-hidden shadow-lg transition-transform transform hover:scale-105 hover:shadow-2xl">
                        <div className="relative">
                            <img
                                src={news.image}
                                alt={news.title}
                                className="w-full h-48 object-cover rounded-t-lg"
                            />
                            <div className="absolute bottom-0 left-0 right-0 p-4 bg-gradient-to-t from-black via-transparent to-transparent">
                                <h2 className="text-xl font-semibold text-white">{news.title}</h2>
                                <p className="text-sm text-gray-300">{news.date}</p>
                            </div>
                        </div>
                        <div className="p-4">
                            <p className="text-gray-700 mb-4">{news.description}</p>
                        </div>
                    </div>
                ))}
            </div>

            <div>
                <div className="2xl:mx-auto 2xl:container mx-4 py-16">
                    <div className="w-full relative flex items-center justify-center">
                        <img
                            src="https://i.ibb.co/4sYZ8gC/img-2.png"
                            alt="dining"
                            className="w-full h-full absolute z-0 hidden xl:block"
                        />
                        <img
                            src="https://i.ibb.co/bbS3J9C/pexels-max-vakhtbovych-6301182-1.png"
                            alt="dining"
                            className="w-full h-full absolute z-0 hidden sm:block xl:hidden"
                        />
                        <img
                            src="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pexels.com%2Fru-ru%2Fsearch%2F%25D1%2584%25D0%25BE%25D0%25BD%2520%25D1%2581%25D0%25B0%25D0%25B9%25D1%2582%25D0%25B0%2F&psig=AOvVaw3A3wSYXvXYV7PZRNzEZDXe&ust=1722515441832000&source=images&cd=vfe&opi=89978449&ved=0CAMQjB1qFwoTCJCw7p-k0YcDFQAAAAAdAAAAABAE"
                            alt="dining"
                            className="w-full h-full absolute z-0 sm:hidden"
                        />
                        <div className="bg-gray-800 bg-opacity-80 md:my-16 lg:py-16 py-10 w-full md:mx-24 md:px-12 px-4 flex flex-col items-center justify-center relative z-40">
                            <h1 className="text-4xl font-semibold leading-9 text-white text-center">Don’t miss out!</h1>
                            <p className="text-base leading-normal text-center text-white mt-6">
                                Subscribe to your newsletter to stay in the loop. Our newsletter is sent once in <br />
                                a week on every friday so subscribe to get latest news and updates.
                            </p>
                            <div className="sm:border border-white flex-col sm:flex-row flex items-center lg:w-5/12 w-full mt-12 space-y-4 sm:space-y-0">
                                <input
                                    className="border border-white sm:border-transparent text-base w-full font-medium leading-none text-white p-4 focus:outline-none bg-transparent placeholder-white"
                                    placeholder="Email Address"
                                />
                                <button
                                    className="focus:outline-none focus:ring-offset-2 focus:ring border border-white sm:border-transparent w-full sm:w-auto bg-white py-4 px-6 hover:bg-opacity-75"
                                >
                                    Subscribe
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}