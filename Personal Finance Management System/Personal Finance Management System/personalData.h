#include <iostream>

struct personalData
{
private:
	std::string name{};
	std::string surname{};
	std::string patronomic{};
	uint16_t* dayOfBirth = new uint16_t{};
	uint16_t* monthOfBirth = new uint16_t{};
	uint16_t* yearOfBirth = new uint16_t{};
public:
	personalData(std::string&, std::string&, std::string&, uint16_t&, uint16_t&, uint16_t&);

	std::string getName() const;
	std::string getSurname() const; 
	std::string getPatronomic() const;
	uint16_t getDayOfBirth() const;
	uint16_t getMonthOfBirth() const;
	uint16_t getYearOfBirth() const;

	~personalData();
};