#include "wallet.h"
#include <sstream>
#include <string>
#include <regex>
#pragma once

namespace functions
{
	template<typename T>
	void saveInFile(T data, std::string fileName)
	{
		fileName += ".txt";

		std::ofstream file(fileName, std::ios::app);

		if (file.is_open())
			file << data;
		else throw std::invalid_argument("File not found!");

		file.close();
	}

	template <typename T>
	void loadFromFile(T**& downloadData, std::string fileName)
	{
		fileName += ".txt";

		std::ifstream file("fileName", std::ios::in);

		std::string data{};
		std::stringstream ss;

		if (file.is_open())
		{
			while (std::getline(file, data))
			{
				ss << data;
			}

		}

		else throw std::invalid_argument("File not found!");
	}

	void formationOfRatings(wallet wallets);
	void top3cost(wallet wallets);
	void top3category(wallet wallets);
	int getCurrentYear();
	void myCheck(std::string& str, std::regex regexCheck);
	wallet* addWallet();
	Transaction* addTransaction();
	personalData* addPersonalData();
	dateOfExpiry* addDateOfExpiry();
};