#include <iostream>
#include <fstream>
#pragma once

class dateOfExpiry
{
private:
	uint16_t* monthOfExpiry;
	uint16_t* yearOFExpiry;
public:
	dateOfExpiry(uint16_t&, uint16_t&);

	dateOfExpiry();

	dateOfExpiry(const dateOfExpiry& _other);

	friend std::ostream& operator << (std::ostream& os, dateOfExpiry _dateOfExpiry)
	{
		if (typeid(os) == typeid(std::ofstream))
		{
			os
				<< *_dateOfExpiry.monthOfExpiry << std::endl
				<< *_dateOfExpiry.yearOFExpiry << std::endl;
		}
		os << "Date of Expiry: " << *_dateOfExpiry.monthOfExpiry << '.' << *_dateOfExpiry.yearOFExpiry << std::endl;
		return os;
	}

	friend std::istream& operator>>(std::istream& is, dateOfExpiry& _dateOfExpiry)
	{
		is >> *_dateOfExpiry.monthOfExpiry >> *_dateOfExpiry.yearOFExpiry;
		return is;
	}

	uint16_t getMonthOfExpiry() const;
	uint16_t getYearOfExpiry() const;

	~dateOfExpiry();
};

